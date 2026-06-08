using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBoxPresenter : IBuyBoxProvider
{
    private readonly BuyBoxModel _model;
    private readonly BuyBoxView _view;

    public BuyBoxPresenter(BuyBoxModel model, BuyBoxView view)
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
        _model.OnSetStartPosition += _view.SetStartPosition;
    }

    private void DeactivateEvents()
    {
        _model.OnSetStartPosition -= _view.SetStartPosition;
    }

    #region Input

    public void MoveToCenter(float duration, Action OnComplete = null) => _view.MoveToCenter(duration, OnComplete);

    public void MoveToCenterDown(float duration, Action OnComplete = null) => _view.MoveToCenterDown(duration, OnComplete);

    public void OpenClose(bool value) => _view.OpenClose(value);

    public void SetScale(float value) => _view.SetScale(value);

    public void ScaleTo(float value, float duration, Action OnComplete = null) => _view.ScaleTo(value, duration, OnComplete);

    public void MoveTo(Vector3 vector, float duration, Action OnComplete = null) => _view.MoveTo(vector, duration, OnComplete);


    public void OpenCover(float duration, Action OnComplete = null) => _view.OpenCover(duration, OnComplete);

    public void CloseCover(float duration, Action OnComplete = null) => _view.CloseCover(duration, OnComplete);

    #endregion
}

public interface IBuyBoxProvider
{
    public void MoveToCenter(float duration, Action OnComplete = null);

    public void MoveToCenterDown(float duration, Action OnComplete = null);

    public void OpenClose(bool value);

    public void SetScale(float value);

    public void ScaleTo(float value, float duration, Action OnComplete = null);

    public void MoveTo(Vector3 vector, float duration, Action OnComplete = null);


    public void OpenCover(float duration, Action OnComplete = null);

    public void CloseCover(float duration, Action OnComplete = null);
}
