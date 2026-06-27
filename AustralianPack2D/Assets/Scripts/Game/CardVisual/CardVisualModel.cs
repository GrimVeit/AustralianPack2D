using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisualModel
{
    private readonly IStoreCardListener _storeCardListener;

    public CardVisualModel(IStoreCardListener storeCardListener)
    {
        _storeCardListener = storeCardListener;
    }

    public void Initialize()
    {
        _storeCardListener.OnOpenCard += OpenCard;
        _storeCardListener.OnCloseCard += CloseCard;
    }

    public void Dispose()
    {
        _storeCardListener.OnOpenCard -= OpenCard;
        _storeCardListener.OnCloseCard -= CloseCard;
    }

    private void OpenCard(Card card, bool isNew)
    {
        OnOpenCard?.Invoke(card, isNew);
    }

    public void CloseCard(Card card)
    {
        OnCloseCard?.Invoke(card);
    }

    #region Output

    public event Action<Card, bool> OnOpenCard;
    public event Action<Card> OnCloseCard;

    #endregion
}
