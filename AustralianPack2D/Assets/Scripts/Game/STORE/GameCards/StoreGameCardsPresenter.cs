using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGameCardsPresenter : IStoreGameCardsProvider
{
    private readonly StoreGameCardsModel _model;

    public StoreGameCardsPresenter(StoreGameCardsModel model)
    {
        _model = model;
    }

    #region Input

    public IReadOnlyList<CardDto> CreateCards(int count) => _model.CreateCards(count);

    #endregion
}

public interface IStoreGameCardsProvider
{
    public IReadOnlyList<CardDto> CreateCards(int count);
}
