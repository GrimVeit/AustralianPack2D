using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMoneyGiftModel
{
    private readonly IMoneyProvider _moneyProvider;
    private int _countMoney = 0;

    public GameMoneyGiftModel(IMoneyProvider moneyProvider)
    {
        _moneyProvider = moneyProvider;
    }

    public void AddGift(int count)
    {
        _countMoney += count;

        OnChangeMoney?.Invoke(_countMoney);
    }

    public void AddGift(GameLevel level)
    {
        _countMoney += level switch
        {
            GameLevel.Level1_4 => 10,
            GameLevel.Level2_8 => 25,
            GameLevel.Level3_16 => 60,
            GameLevel.Level4_32 => 150,
            GameLevel.Level5_64 => 300,
            _ => 10,
        };
        OnChangeMoney?.Invoke(_countMoney);
    }

    public void SendGift()
    {
        _moneyProvider.SendMoney(_countMoney);
    }

    #region Output

    public event Action<int> OnChangeMoney;

    #endregion
}
