using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperCardBoxType
{
    private static readonly System.Random _random = new();

    public static List<CardType> GetCards(CardBoxType boxType, int count)
    {
        var result = new List<CardType>(count);

        for (int i = 0; i < count; i++)
        {
            result.Add(GetRandomType(boxType));
        }

        return result;
    }

    private static CardType GetRandomType(CardBoxType boxType)
    {
        int roll = _random.Next(1, 101);

        return boxType switch
        {
            CardBoxType.Standard => roll switch
            {
                <= 65 => CardType.Common,
                <= 85 => CardType.Uncommon,
                <= 95 => CardType.Rare,
                <= 99 => CardType.Epic,
                _ => CardType.Mythical
            },

            CardBoxType.Priority => roll switch
            {
                <= 35 => CardType.Common,
                <= 65 => CardType.Uncommon,
                <= 85 => CardType.Rare,
                <= 95 => CardType.Epic,
                _ => CardType.Mythical
            },

            _ => CardType.Common
        };
    }
}
