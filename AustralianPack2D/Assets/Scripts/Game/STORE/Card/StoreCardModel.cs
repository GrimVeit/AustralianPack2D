using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreCardModel
{
    public event Action<Card> OnOpenCard;
    public event Action<Card> OnCloseCard;

    private readonly List<Card> _cards;
    private readonly Dictionary<CardKey, CardSaveData> _save = new();

    public string FilePath = Path.Combine(Application.persistentDataPath, "Cards.json");

    private readonly System.Random _random = new();

    public StoreCardModel(CardPacksSO chipGroup)
    {
        _cards = new List<Card>();

        // build runtime cards
        foreach (var pack in chipGroup.CardPackSOs)
        {
            foreach (var card in pack.cards)
            {
                _cards.Add(new Card(
                    card.sprite,
                    card.page,
                    card.index,
                    pack.type
                ));
            }
        }

        Load();
    }

    private void Load()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            var wrapper = JsonUtility.FromJson<CardSaveWrapper>(json);

            foreach (var e in wrapper.Entries)
            {
                var key = new CardKey(e.Type, e.Page, e.Index);

                _save[key] = new CardSaveData
                {
                    IsOpen = e.IsOpen
                };
            }
        }
        else
        {
            foreach (var card in _cards)
            {
                _save[card.Key] = new CardSaveData
                {
                    IsOpen = false
                };
            }

            // стартовая карта открыта
            if (_cards.Count > 0)
            {
                _save[_cards[0].Key].IsOpen = true;
            }
        }
    }

    public void Initialize()
    {
        foreach (var card in _cards)
        {
            if (_save.TryGetValue(card.Key, out var data) && data.IsOpen)
                OnOpenCard?.Invoke(card);
            else
                OnCloseCard?.Invoke(card);
        }
    }

    public void Dispose()
    {
        var wrapper = new CardSaveWrapper();

        foreach (var kvp in _save)
        {
            wrapper.Entries.Add(new CardSaveEntry
            {
                Type = kvp.Key.Type,
                Page = kvp.Key.Page,
                Index = kvp.Key.Index,
                IsOpen = kvp.Value.IsOpen
            });
        }

        var json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(FilePath, json);
    }

    #region INPUT

    public void OpenCard(CardType type, int page, int index)
    {
        var key = new CardKey(type, page, index);

        if (!_save.TryGetValue(key, out var data))
        {
            Debug.LogError($"Card not found: {type} {page} {index}");
            return;
        }

        if (data.IsOpen)
            return;

        data.IsOpen = true;

        var card = GetCard(key);
        if (card != null)
            OnOpenCard?.Invoke(card);
    }

    public Card GetRandomCard()
    {
        if (_cards.Count == 0)
            return null;

        return _cards[_random.Next(_cards.Count)];
    }

    public List<Card> GetRandomCards(int count)
    {
        var result = new List<Card>();

        if (_cards.Count == 0)
            return result;

        for (int i = 0; i < count; i++)
        {
            result.Add(GetRandomCard());
        }

        return result;
    }

    #endregion

    private Card GetCard(CardKey key)
    {
        return _cards.Find(c => c.Key.Equals(key));
    }
}

[Serializable]
public struct CardKey
{
    public CardType Type;
    public int Page;
    public int Index;

    public CardKey(CardType type, int page, int index)
    {
        Type = type;
        Page = page;
        Index = index;
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

[Serializable]
public class CardSaveData
{
    public bool IsOpen;
}
