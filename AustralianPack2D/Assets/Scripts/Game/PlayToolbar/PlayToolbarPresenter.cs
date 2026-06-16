using System;

public class PlayToolbarPresenter
{
    private readonly PlayToolbarModel _model;
    private readonly PlayToolbarView _view;

    public PlayToolbarPresenter(PlayToolbarModel model, PlayToolbarView view)
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
        _view.OnClickToToolbar += _model.ClickToolbar;
        _view.OnClickToRestart += _model.ClickRestart;
        _view.OnClickToExit += _model.ClickExit;

        _model.OnShowToolbar += _view.ShowToolbar;
        _model.OnHideToolbar += _view.HideToolbar;
    }

    private void DeactivateEvents()
    {
        _view.OnClickToToolbar -= _model.ClickToolbar;
        _view.OnClickToRestart -= _model.ClickRestart;
        _view.OnClickToExit -= _model.ClickExit;

        _model.OnShowToolbar -= _view.ShowToolbar;
        _model.OnHideToolbar -= _view.HideToolbar;
    }

    #region Output

    public event Action OnClickToExit
    {
        add => _model.OnClickToExit += value;
        remove => _model.OnClickToExit -= value;
    }

    public event Action OnClickToRestart
    {
        add => _model.OnClickToRestart += value;
        remove => _model.OnClickToRestart -= value;
    }

    #endregion

    #region Input

    public void ShowToolbar() => _view.ShowToolbar();
    public void HideToolbar() => _view.HideToolbar();

    #endregion
}
