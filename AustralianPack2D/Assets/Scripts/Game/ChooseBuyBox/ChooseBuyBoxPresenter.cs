using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuyBoxPresenter : IChooseBuyBoxProvider, IChooseBuyBoxListener
{
    private readonly ChooseBuyBoxModel _model;
    private readonly ChooseBuyBoxView _view;

    public ChooseBuyBoxPresenter(ChooseBuyBoxModel model, ChooseBuyBoxView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseBox += _model.Choose;

        _model.OnChoose += _view.Choose;
        _model.OnUnchoose += _view.Unchoose;
        _model.OnShowAll += _view.ShowAll;
        _model.OnHideAll += _view.HideAll;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseBox -= _model.Choose;

        _model.OnChoose -= _view.Choose;
        _model.OnUnchoose -= _view.Unchoose;
        _model.OnShowAll -= _view.ShowAll;
        _model.OnHideAll -= _view.HideAll;
    }

    #region Output

    public event Action<int> OnChoose
    {
        add => _model.OnChoose += value;
        remove => _model.OnChoose -= value;
    }

    public event Action<int> OnUnchoose
    {
        add => _model.OnUnchoose += value;
        remove => _model.OnUnchoose -= value;
    }

    #endregion

    #region Input

    public void ShowAll() => _model.ShowAll();
    public void HideAll() => _model.HideAll();

    #endregion
}

public interface IChooseBuyBoxListener
{
    public event Action<int> OnChoose;
    public event Action<int> OnUnchoose;
}

public interface IChooseBuyBoxProvider
{
    void ShowAll();
    void HideAll();
}
