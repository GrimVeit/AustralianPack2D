using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPresentationPresenter
{
    private readonly CardPresentationModel _model;
    private readonly CardPresentationView _view;

    public CardPresentationPresenter(CardPresentationModel model, CardPresentationView view)
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
        _model.OnSetCard += _view.CardPresentation;
    }

    private void DeactivateEvents()
    {
        _model.OnSetCard -= _view.CardPresentation;
    }
}
