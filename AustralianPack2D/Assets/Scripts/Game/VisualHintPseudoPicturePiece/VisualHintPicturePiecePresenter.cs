using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualHintPicturePiecePresenter
{
    private readonly VisualHintPicturePieceModel _model;
    private readonly VisualHintPicturePieceView _view;

    public VisualHintPicturePiecePresenter(VisualHintPicturePieceModel model, VisualHintPicturePieceView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnShow += _view.Show;
        _model.OnHide += _view.Hide;
    }

    private void DeactivateEvents()
    {
        _model.OnShow -= _view.Show;
        _model.OnHide -= _view.Hide;
    }
}
