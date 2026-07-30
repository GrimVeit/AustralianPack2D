using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPresentationModel
{
    private readonly ICardVisualListener _cardVisualListener;

    public CardPresentationModel(ICardVisualListener cardVisualListener)
    {
        _cardVisualListener = cardVisualListener;

        _cardVisualListener.OnClickCard_Value += SetCard;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _cardVisualListener.OnClickCard_Value -= SetCard;
    }

    private void SetCard(Card card)
    {
        OnSetCard?.Invoke(card.Sprite);
    }

    #region Output

    public event Action<Sprite> OnSetCard;

    #endregion
}
