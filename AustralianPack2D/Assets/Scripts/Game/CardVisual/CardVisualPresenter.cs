using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisualPresenter
{
    private readonly CardVisualModel _model;
    private readonly CardVisualView _view;

    public CardVisualPresenter(CardVisualModel model, CardVisualView view)
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
        _model.OnOpenCard += _view.OpenCard;
        _model.OnCloseCard += _view.CloseCard;
    }

    private void DeactivateEvents()
    {
        _model.OnOpenCard -= _view.OpenCard;
        _model.OnCloseCard -= _view.CloseCard;
    }
}
