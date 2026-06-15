using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualChickenEffectPresenter : IVisualChickenEffectProvider
{
    private readonly VisualChickenEffectModel _model;
    private readonly VisualChickenEffectView _view;

    public VisualChickenEffectPresenter(VisualChickenEffectModel model, VisualChickenEffectView view)
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
        _model.OnChoose += _view.Activate;
    }

    private void DeactivateEvents()
    {
        _model.OnChoose -= _view.Activate;
    }

    #region Input

    public void DeactivateVisual() => _view.Deactivate();

    #endregion
}

public interface IVisualChickenEffectProvider
{
    void DeactivateVisual();
}
