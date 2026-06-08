using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBattlePresenter : IChickenBattleProvider, IChickenBattleListener
{
    private readonly ChickenBattleModel _model;

    public ChickenBattlePresenter(ChickenBattleModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Output

    public event Action OnWin
    {
        add => _model.OnWin += value;
        remove => _model.OnWin -= value;
    }

    public event Action OnLose
    {
        add => _model.OnLose += value;
        remove => _model.OnLose -= value;
    }

    public event Action OnEndGame
    {
        add => _model.OnEndGame += value;
        remove => _model.OnEndGame -= value;
    }

    public event Action<IChickenUnit> OnLeaderChanged
    {
        add => _model.OnLeaderChanged += value;
        remove => _model.OnLeaderChanged -= value;
    }

    #endregion

    #region Input

    public void StartGame() => _model.StartGame();
    public void CheckWinner() => _model.CheckWinner();
    public void StopBattle() => _model.StopBattle();

    #endregion
}

public interface IChickenBattleListener
{
    public event Action OnWin;
    public event Action OnLose;

    public event Action OnEndGame;

    public event Action<IChickenUnit> OnLeaderChanged;
}

public interface IChickenBattleProvider
{
    void StartGame();
    void CheckWinner();
    void StopBattle();
}
