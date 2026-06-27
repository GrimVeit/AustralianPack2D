using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreCardModel
{
    public event Action<Card, bool> OnOpenCard;
    public event Action<Card> OnCloseCard;

    private readonly List<Card> _cards;
    private readonly Dictionary<CardKey, Card> _cardsMap;
    private readonly Dictionary<CardKey, bool> _save;

    private readonly string _filePath;
    private readonly System.Random _random = new();

    private readonly string XOR_KEY = "eurghfuirehfisdfioerfywre73647898037uhgdg";

    public StoreCardModel(CardPacksSO chipGroup)
    {
        _cards = new List<Card>();
        _cardsMap = new Dictionary<CardKey, Card>();
        _save = new Dictionary<CardKey, bool>();

        _filePath = Path.Combine(Application.persistentDataPath, "Cards.json");

        foreach (var pack in chipGroup.CardPackSOs)
        {
            foreach (var card in pack.cards)
            {
                var runtimeCard = new Card(
                    card.sprite,
                    card.page,
                    card.index,
                    pack.type
                );

                _cards.Add(runtimeCard);
                _cardsMap[runtimeCard.Key] = runtimeCard;
            }
        }

        Load();
    }

    #region LOAD / SAVE

    private void Load()
    {
        if (!File.Exists(_filePath))
        {
            CreateDefault();
            return;
        }

        try
        {
            var encrypted = File.ReadAllText(_filePath);
            var json = Xor(encrypted, XOR_KEY);

            var wrapper = JsonUtility.FromJson<CardSaveWrapper>(json);

            if (wrapper?.Entries == null)
            {
                CreateDefault();
                return;
            }

            foreach (var e in wrapper.Entries)
            {
                if (e == null) continue;

                var key = new CardKey(e.Type, e.Page, e.Index);
                _save[key] = e.IsOpen;
            }
        }
        catch
        {
            // любой мусор, битый JSON, обрезанный файл и т.д.
            CreateDefault();
        }
    }

    public void Save()
    {
        var wrapper = new CardSaveWrapper();

        foreach (var kvp in _save)
        {
            wrapper.Entries.Add(new CardSaveEntry
            {
                Type = kvp.Key.Type,
                Page = kvp.Key.Page,
                Index = kvp.Key.Index,
                IsOpen = kvp.Value
            });
        }

        var json = JsonUtility.ToJson(wrapper);

        var encrypted = Xor(json, XOR_KEY);

        File.WriteAllText(_filePath, encrypted);
    }

    private void CreateDefault()
    {
        _save.Clear();

        foreach (var card in _cards)
        {
            _save[card.Key] = false;
        }
    }

    private string Xor(string data, string key)
    {
        var result = new char[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            result[i] = (char)(data[i] ^ key[i % key.Length]);
        }

        return new string(result);
    }

    #endregion

    #region INIT

    public void Initialize()
    {
        foreach (var card in _cards)
        {
            bool isOpen = _save.TryGetValue(card.Key, out var state) && state;

            if (isOpen)
                OnOpenCard?.Invoke(card, false);
            else
                OnCloseCard?.Invoke(card);
        }
    }

    #endregion

    #region INPUT

    public void OpenCard(CardType type, int page, int index)
    {
        var key = new CardKey(type, page, index);

        if (!_save.TryGetValue(key, out var isOpen))
        {
            Debug.LogError($"Card not found: {type} {page} {index}");
            return;
        }

        if (isOpen) return;

        _save[key] = true;

        if (_cardsMap.TryGetValue(key, out var card))
        {
            OnOpenCard?.Invoke(card, true);
        }
    }

    #endregion

    #region RANDOM

    public Card GetRandomCard()
    {
        if (_cards.Count == 0)
            return null;

        return _cards[_random.Next(_cards.Count)];
    }

    public List<Card> GetRandomCards(int count)
    {
        if (_cards.Count == 0)
            return new List<Card>();

        var list = new List<Card>(_cards);

        count = Mathf.Min(count, list.Count);

        for (int i = 0; i < count; i++)
        {
            int r = _random.Next(i, list.Count);
            (list[i], list[r]) = (list[r], list[i]);
        }

        return list.Take(count).ToList();
    }

    #endregion
}

[Serializable]
public readonly struct CardKey : IEquatable<CardKey>
{
    public readonly CardType Type;
    public readonly int Page;
    public readonly int Index;

    public CardKey(CardType type, int page, int index)
    {
        Type = type;
        Page = page;
        Index = index;
    }

    public readonly bool Equals(CardKey other)
    {
        return Type == other.Type &&
               Page == other.Page &&
               Index == other.Index;
    }

    public override readonly bool Equals(object obj)
    {
        return obj is CardKey other && Equals(other);
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Type, Page, Index);
    }
}

public class Card
{
    public Sprite Sprite { get; }
    public int Page { get; }
    public int Index { get; }
    public CardType Type { get; }

    public Card(Sprite sprite, int page, int index, CardType type)
    {
        Sprite = sprite;
        Page = page;
        Index = index;
        Type = type;
    }

    public CardKey Key => new(Type, Page, Index);
}

[Serializable]
public class CardSaveWrapper
{
    public List<CardSaveEntry> Entries = new();
}

[Serializable]
public class CardSaveEntry
{
    public CardType Type;
    public int Page;
    public int Index;
    public bool IsOpen;
}
