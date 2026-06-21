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

    public event Action<IGameCard> OnDestroyCard
    {
        add => _view.OnDestroyCard += value;
        remove => _view.OnDestroyCard -= value;
    }

    #endregion

    #region Input

    public void CreateGrid(GameLevel level, IReadOnlyList<CardDto> cardDtos, int design) => _view.Spawn(level, cardDtos, design);

    #endregion
}

public interface ICardsGameSpawnerProvider
{
    public void CreateGrid(GameLevel level, IReadOnlyList<CardDto> cardDtos, int design);
}

public interface ICardsGameSpawnerListener
{
    public event Action<IGameCard> OnDestroyCard;
    public event Action<IReadOnlyList<IGameCard>> OnSpawnedCards;
}
