using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseChickenPresenter : IChooseChickenProvider, IChooseChickenListener
{
    private readonly ChooseChickenModel _model;
    private readonly ChooseChickenView _view;

    public ChooseChickenPresenter(ChooseChickenModel model, ChooseChickenView view)
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
        _view.OnChooseChicken += _model.Choose;
        _view.OnSetSoundOpen += _model.SetOpenSound;

        _model.OnSetTypes += _view.SetTypes;
        _model.OnChoose += _view.Choose;
        _model.OnUnchoose += _view.Unchoose;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseChicken -= _model.Choose;
        _view.OnSetSoundOpen -= _model.SetOpenSound;

        _model.OnSetTypes -= _view.SetTypes;
        _model.OnChoose -= _view.Choose;
        _model.OnUnchoose -= _view.Unchoose;
    }

    #region Output

    public event Action<ChickenType> OnChoose
    {
        add => _model.OnChoose += value;
        remove => _model.OnChoose -= value;
    }
    public event Action<ChickenType> OnUnchoose
    {
        add => _model.OnUnchoose += value;
        remove => _model.OnUnchoose -= value;
    }

    #endregion

    #region Input

    public void ActivateAll() => _view.ActivateAll();
    public void DeactivateAll() => _view.DeactivateAll();

    public void ShowAll() => _view.ShowAll();
    public void HideAll() => _view.HideAll();

    #endregion
}

public interface IChooseChickenListener
{
    public event Action<ChickenType> OnChoose;
    public event Action<ChickenType> OnUnchoose;
}

public interface IChooseChickenProvider
{
    public void ActivateAll();
    public void DeactivateAll();

    public void ShowAll();
    public void HideAll();
}
