using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScorePresenter : IGameScoreListener, IGameScoreInfo
{
    private readonly GameScoreModel _model;
    private readonly GameScoreView _view;

    public GameScorePresenter(GameScoreModel model, GameScoreView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnChangeMoves += _view.SetMove;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeMoves -= _view.SetMove;
    }

    #region Output

    public event Action OnFinish
    {
        add => _model.OnFinish += value;
        remove => _model.OnFinish -= value;
    }

    #endregion

    #region Input

    public CardBoxType CardBoxType => _model.CardBoxType;

    #endregion
}

public interface IGameScoreInfo
{
    public CardBoxType CardBoxType { get; }
}

public interface IGameScoreListener
{
    public event Action OnFinish;
}
