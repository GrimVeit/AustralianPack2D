using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskEffectPresenter : IMaskEffectProvider
{
    private readonly MaskEffectModel _model;
    private readonly MaskEffectView _view;

    public MaskEffectPresenter(MaskEffectModel model, MaskEffectView view)
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
    }

    private void ActivateEvents()
    {

    }

    private void DeactivateEvents()
    {

    }

    #region Input

    public void Play(string id, Action OnComplete = null) => _view.Play(id, OnComplete);
    public void Stop(string id) => _view.Stop(id);

    #endregion
}

public interface IMaskEffectProvider
{
    void Play(string id, Action OnComplete = null);
    void Stop(string id);
}
