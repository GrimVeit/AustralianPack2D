using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountChickenPicturePresenter : ICountChickenPictureProvider
{
    private readonly CountChickenPictureModel _model;
    private readonly CountChickenPictureView _view;

    public CountChickenPicturePresenter(CountChickenPictureModel model, CountChickenPictureView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnShowCount += _view.SetData;
    }

    private void DeactivateEvents()
    {
        _model.OnShowCount -= _view.SetData;
    }

    #region Input

    public void ShowCount() => _model.ShowCount();

    #endregion
}

public interface ICountChickenPictureProvider
{
    void ShowCount();
}
