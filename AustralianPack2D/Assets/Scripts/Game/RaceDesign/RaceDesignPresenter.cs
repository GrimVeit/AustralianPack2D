using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceDesignPresenter
{
    private readonly RaceDesignModel _model;
    private readonly RaceDesignView _view;

    public RaceDesignPresenter(RaceDesignModel model, RaceDesignView view)
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
        _model.OnSetLocation += _view.SetLocation;
    }

    private void DeactivateEvents()
    {
        _model.OnSetLocation -= _view.SetLocation;
    }
}
