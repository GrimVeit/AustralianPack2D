using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChickenPicturePresenter : IShowChickenPictureListener
{
    private readonly ShowChickenPictureModel _model;
    private readonly ShowChickenPictureView _view;

    public ShowChickenPicturePresenter(ShowChickenPictureModel model, ShowChickenPictureView view)
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
        _model.OnSetData += _view.SetData;
    }

    private void DeactivateEvents()
    {
        _model.OnSetData -= _view.SetData;
    }

    #region Output

    public event Action OnShowFullPicture
    {
        add => _model.OnShowFullPicture += value;
        remove => _model.OnShowFullPicture -= value;
    }

    public event Action OnShowNotFullPicture
    {
        add => _model.OnShowNotFullPicture += value;
        remove => _model.OnShowNotFullPicture -= value;
    }

    #endregion
}

public interface IShowChickenPictureListener
{
    public event Action OnShowFullPicture;
    public event Action OnShowNotFullPicture;
}
