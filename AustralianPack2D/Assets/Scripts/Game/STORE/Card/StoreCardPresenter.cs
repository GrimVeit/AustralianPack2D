using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCardPresenter : IStoreCardProvider, IStoreCardListener
{
    private readonly StoreCardModel _model;

    public StoreCardPresenter(StoreCardModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Save();
    }

    #region Output

    public event Action<Card, bool> OnOpenCard
    {
        add => _model.OnOpenCard += value;
        remove => _model.OnOpenCard -= value;
    }

    public event Action<Card> OnCloseCard
    {
        add => _model.OnCloseCard += value;
        remove => _model.OnCloseCard -= value;
    }

    #endregion

    #region Input

    public void Save() => _model.Save();
    public void OpenCard(CardType type, int page, int index) => _model.OpenCard(type, page, index);
    public Card GetRandomCard() => _model.GetRandomCard();
    public List<Card> GetRandomCards(int count) => _model.GetRandomCards(count);

    #endregion
}

public interface IStoreCardProvider
{
    public void OpenCard(CardType type, int page, int index);
    public Card GetRandomCard();
    public List<Card> GetRandomCards(int count);
}

public interface IStoreCardListener
{
    public event Action<Card, bool> OnOpenCard;
    public event Action<Card> OnCloseCard;
}
