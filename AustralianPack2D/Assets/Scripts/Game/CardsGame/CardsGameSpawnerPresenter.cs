using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGameSpawnerPresenter : ICardsGameSpawnerProvider, ICardsGameSpawnerListener
{
    private readonly CardsGameSpawnerView _view;

    public CardsGameSpawnerPresenter(CardsGameSpawnerView view)
    {
        _view = view;
    }

    #region Output

    public event Action<IReadOnlyList<IGameCard>> OnSpawnedCards
    {
        add => _view.OnSpawnedCards += value;
        remove => _view.OnSpawnedCards -= value;
    }

    #endregion

    #region Input

    public void CreateGrid(GameLevel level, IReadOnlyList<CardDto> cardDtos) => _view.Spawn(level, cardDtos);

    #endregion
}

public interface ICardsGameSpawnerProvider
{
    public void CreateGrid(GameLevel level, IReadOnlyList<CardDto> cardDtos);
}

public interface ICardsGameSpawnerListener
{
    public event Action<IReadOnlyList<IGameCard>> OnSpawnedCards;
}
