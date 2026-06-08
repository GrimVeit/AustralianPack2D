using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRaceLeaderModel
{
    private readonly IChickenBattleListener _chickenBattleListener;

    public ChickenRaceLeaderModel(IChickenBattleListener chickenBattleListener)
    {
        _chickenBattleListener = chickenBattleListener;
    }

    public void Initialize()
    {
        _chickenBattleListener.OnLeaderChanged += SetLeader;
    }

    public void Dispose()
    {
        _chickenBattleListener.OnLeaderChanged -= SetLeader;
    }

    private void SetLeader(IChickenUnit chickenUnit)
    {
        OnLeaderChanged?.Invoke(chickenUnit);
    }

    #region Output

    public event Action<IChickenUnit> OnLeaderChanged;

    #endregion
}
