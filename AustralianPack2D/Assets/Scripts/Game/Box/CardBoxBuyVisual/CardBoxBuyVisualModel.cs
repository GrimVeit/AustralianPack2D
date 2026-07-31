using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoxBuyVisualModel
{
    private readonly ICardBoxBuyProvider _cardBoxBuyProvider;

    private readonly IMoneyProvider _moneyProvider;

    public CardBoxBuyVisualModel(ICardBoxBuyProvider cardBoxBuyProvider, IMoneyProvider moneyProvider)
    {
        _cardBoxBuyProvider = cardBoxBuyProvider;
        _moneyProvider = moneyProvider;
    }

    public void BuyBox(CardBoxType type, int cost)
    {
        if (_moneyProvider.CanAfford(cost))
        {
            _moneyProvider.SendMoney(-cost);

            _cardBoxBuyProvider.Buy(type);

            OnCardBoxBuy?.Invoke();
        }
    }

    #region Output

    public event Action OnCardBoxBuy;

    #endregion
}
