using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGameSpawnerPresenter : ICardsGameSpawnerProvider
{
    private readonly CardsGameSpawnerView _view;

    public CardsGameSpawnerPresenter(CardsGameSpawnerView view)
    {
        _view = view;
    }

    #region Input

    public void CreateGrid(GameLevel level, IReadOnlyList<CardDto> cardDtos) => _view.Spawn(level, cardDtos);

    #endregion
}

public interface ICardsGameSpawnerProvider
{
    public void CreateGrid(GameLevel level, IReadOnlyList<CardDto> cardDtos);
}
