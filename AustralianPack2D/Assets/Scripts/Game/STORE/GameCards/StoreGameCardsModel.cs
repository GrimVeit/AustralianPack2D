using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreGameCardsModel
{
    private readonly List<CardDefinition> _catalog;

    public StoreGameCardsModel(List<Sprite> sprites)
    {
        _catalog = new List<CardDefinition>(sprites.Count);

        for (int i = 0; i < sprites.Count; i++)
        {
            _catalog.Add(new CardDefinition(i, sprites[i]));
        }
    }

    public IReadOnlyList<CardDto> CreateCards(int count)
    {
        if (count <= 0)
            return new List<CardDto>();

        if (count % 2 != 0)
            return new List<CardDto>();

        int uniqueCount = count / 2;

        if (uniqueCount > _catalog.Count)
            return new List<CardDto>();

        var selected = _catalog
            .OrderBy(_ => UnityEngine.Random.value)
            .Take(uniqueCount)
            .ToList();

        List<CardDto> result = new(count);

        foreach (var def in selected)
        {
            // создаём пару
            result.Add(new CardDto(def.Id, def.Sprite));
            result.Add(new CardDto(def.Id, def.Sprite));
        }

        return result
            .OrderBy(_ => UnityEngine.Random.value)
            .ToList();
    }
}

public class CardDefinition
{
    public int Id { get; }
    public Sprite Sprite { get; }

    public CardDefinition(int id, Sprite sprite)
    {
        Id = id;
        Sprite = sprite;
    }
}

public class CardDto
{
    public int PairId { get; }
    public Sprite Sprite { get; }

    public CardDto(int pairId, Sprite sprite)
    {
        PairId = pairId;
        Sprite = sprite;
    }
}
