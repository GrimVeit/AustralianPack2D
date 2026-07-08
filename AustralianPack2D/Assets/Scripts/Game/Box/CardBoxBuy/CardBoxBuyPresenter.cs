using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoxBuyPresenter : ICardBoxBuyProvider, ICardBoxBuyListener
{
    private readonly CardBoxBuyModel _model;

    public CardBoxBuyPresenter(CardBoxBuyModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Output

    public event Action<CardBoxType> OnSendBox
    {
        add => _model.OnSendBox += value;
        remove => _model.OnSendBox -= value;
    }

    #endregion

    #region Input

    public void Buy(CardBoxType type) => _model.Buy(type);
    public void Buy(CardBoxType type, int cost) => _model.Buy(type, cost);

    #endregion
}

public interface ICardBoxBuyProvider
{
    public void Buy(CardBoxType type, int cost);
    public void Buy(CardBoxType type);
}

public interface ICardBoxBuyListener
{
    public event Action<CardBoxType> OnSendBox;
}
