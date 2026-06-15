using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelVisualPresenter
{
    private readonly ChooseLevelVisualModel _model;
    private readonly ChooseLevelVisualView _view;

    public ChooseLevelVisualPresenter(ChooseLevelVisualModel model, ChooseLevelVisualView view)
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
        _view.OnChooseLevel += _model.SetLevel;

        _model.OnLevelSelect += _view.Select;
        _model.OnLevelDeselect += _view.Deselect;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseLevel -= _model.SetLevel;

        _model.OnLevelSelect -= _view.Select;
        _model.OnLevelDeselect -= _view.Deselect;
    }
}
