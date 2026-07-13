using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUniqueCounterModel
{
    private readonly ICardPresentationListener _cardPresentationListener;

    public CardUniqueCounterModel(ICardPresentationListener cardPresentationListener)
    {
        _cardPresentationListener = cardPresentationListener;

        _cardPresentationListener.OnGetCountUniqueCards += GetCountUniqueCards;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _cardPresentationListener.OnGetCountUniqueCards -= GetCountUniqueCards;
    }

    #region Output

    public event Action<int> OnGetCountUniqueCards;
    
    private void GetCountUniqueCards(int count)
    {
        OnGetCountUniqueCards?.Invoke(count);
    }

    #endregion
}
