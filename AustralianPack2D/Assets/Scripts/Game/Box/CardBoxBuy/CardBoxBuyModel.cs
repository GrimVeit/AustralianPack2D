using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoxBuyModel
{
    private readonly IMoneyProvider _moneyProvider;

    public CardBoxBuyModel(IMoneyProvider moneyProvider)
    {
        _moneyProvider = moneyProvider;
    }

    public void Buy(CardBoxType type, int cost)
    {
        if (_moneyProvider.CanAfford(cost))
        {
            _moneyProvider.SendMoney(-cost);
            OnSendBox?.Invoke(type);
        }
    }

    public void Buy(CardBoxType type)
    {
        OnSendBox?.Invoke(type);
    }

    #region Output

    public event Action<CardBoxType> OnSendBox;

    #endregion
}
