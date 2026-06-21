using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGameSpawnerModel
{
    private readonly IStoreCardDesignInfoProvider _cardDesignInfoProvider;

    public void Spawn(GameLevel level, IReadOnlyList<CardDto> cardDtos)
    {
        OnSpawn?.Invoke(level, cardDtos, _cardDesignInfoProvider.CardDesignIndex);
    }

    #region Output

    public event Action<GameLevel, IReadOnlyList<CardDto>, int> OnSpawn;

    #endregion
}
