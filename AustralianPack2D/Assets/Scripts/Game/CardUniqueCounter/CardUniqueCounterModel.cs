using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUniqueCounterModel
{
    private readonly IShopCardPresentationListener _cardPresentationListener;

    public CardUniqueCounterModel(IShopCardPresentationListener cardPresentationListener)
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
