using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreModel
{
    public bool IsGoodResult;

    private readonly ICardsOrchectrationListener _flow;
    private readonly IStoreLevelInfo _levelInfo;

    private int _moves;
    private int _matches;

    private LevelScoreConfig _config;

    private bool _isGoodResult;

    public GameScoreModel(ICardsOrchectrationListener flow, IStoreLevelInfo levelInfo)
    {
        _flow = flow;
        _levelInfo = levelInfo;

        _config = HelperLevelScoreBalance.GetConfig(_levelInfo.GameLevel);

        _flow.OnAddMove += OnMove;
        _flow.OnAddMatch += OnMatch;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _flow.OnAddMove -= OnMove;
        _flow.OnAddMatch -= OnMatch;
    }

    private void OnMove()
    {
        _moves += 1;

        _isGoodResult = _moves <= _config.GoodMoveThreshold;

        if (_isGoodResult)
        {
            OnAddMoves?.Invoke(_moves, true);
        }
        else
        {
            OnAddMoves?.Invoke(_moves, false);
        }
    }

    private void OnMatch()
    {
        _matches += 1;

        if (_matches >= _config.TotalPairs)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        Debug.Log("FINISH GAME");
    }

    #region Output

    public event Action<int, bool> OnAddMoves;

    #endregion
}
