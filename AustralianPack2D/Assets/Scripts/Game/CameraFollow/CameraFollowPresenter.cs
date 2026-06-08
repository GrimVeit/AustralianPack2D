using System.Collections;
using System.Collections.Generic;

public class CameraFollowPresenter : ICameraFollowProvider
{
    private readonly CameraFollowModel _model;
    private readonly CameraFollowView _view;

    public CameraFollowPresenter(CameraFollowModel model, CameraFollowView view)
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
        _model.OnSetChickens += _view.SetChickens;
    }

    private void DeactivateEvents()
    {
        _model.OnSetChickens -= _view.SetChickens;
    }

    #region Input

    public void Clear() => _view.ClearTargets();

    #endregion
}

public interface ICameraFollowProvider
{
    void Clear();
}
