using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsBoxPseudoModel
{
    private readonly ICardBoxBuyListener _cardBoxBuyListener;

    public CardsBoxPseudoModel(ICardBoxBuyListener cardBoxBuyListener)
    {
        _cardBoxBuyListener = cardBoxBuyListener;

        _cardBoxBuyListener.OnSendBox += SetDesign;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _cardBoxBuyListener.OnSendBox -= SetDesign;
    }

    private void SetDesign(CardBoxType type)
    {
        OnSetDesign?.Invoke(type);
    }

    #region Output

    public event Action<CardBoxType> OnSetDesign;

    #endregion
}
