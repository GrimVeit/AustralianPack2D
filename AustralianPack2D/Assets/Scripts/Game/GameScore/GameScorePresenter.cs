using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScorePresenter
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

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnAddMoves += _view.SetMove;
    }

    private void DeactivateEvents()
    {
        _model.OnAddMoves -= _view.SetMove;
    }
}
