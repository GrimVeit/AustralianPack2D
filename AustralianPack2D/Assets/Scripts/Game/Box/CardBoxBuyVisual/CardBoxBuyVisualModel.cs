using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoxBuyVisualModel
{
    private readonly ICardBoxBuyProvider _cardBoxBuyProvider;

    public CardBoxBuyVisualModel(ICardBoxBuyProvider cardBoxBuyProvider)
    {
        _cardBoxBuyProvider = cardBoxBuyProvider;
    }

    public void BuyBox(CardBoxType type, int cost)
    {
        _cardBoxBuyProvider.Buy(type, cost);

        OnCardBoxBuy?.Invoke();
    }

    #region Output

    public event Action OnCardBoxBuy;

    #endregion
}
