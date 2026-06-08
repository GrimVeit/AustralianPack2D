using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRaceLeaderPresenter : IChickenRaceLeaderProvider
{
    private readonly ChickenRaceLeaderModel _model;
    private readonly ChickenRaceLeaderView _view;

    public ChickenRaceLeaderPresenter(ChickenRaceLeaderModel model, ChickenRaceLeaderView view)
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
        _model.OnLeaderChanged += _view.SetLeader;
    }

    private void DeactivateEvents()
    {
        _model.OnLeaderChanged -= _view.SetLeader;
    }

    #region Input

    public void Activate() => _view.Activate();
    public void Deactivate() => _view.Deactivate();

    #endregion
}

public interface IChickenRaceLeaderProvider
{
    void Activate();
    void Deactivate();
}
