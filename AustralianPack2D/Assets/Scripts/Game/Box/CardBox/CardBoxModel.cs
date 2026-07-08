using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoxModel
{
    private readonly ICardBoxBuyListener _cardBoxBuyListener;

    public CardBoxModel(ICardBoxBuyListener cardBoxBuyListener)
    {
        _cardBoxBuyListener = cardBoxBuyListener;

        _cardBoxBuyListener.OnSendBox += SetSkin;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _cardBoxBuyListener.OnSendBox -= SetSkin;
    }

    private void SetSkin(CardBoxType type)
    {
        OnSetSkin?.Invoke(type);
    }

    #region Output

    public event Action<CardBoxType> OnSetSkin;

    #endregion
}
