using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoxBuyModel
{

    public void Buy(CardBoxType type)
    {
        OnSendBox?.Invoke(type);
    }

    #region Output

    public event Action<CardBoxType> OnSendBox;

    #endregion
}
