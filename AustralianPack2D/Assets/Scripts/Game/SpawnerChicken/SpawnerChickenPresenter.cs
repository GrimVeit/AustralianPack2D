using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerChickenPresenter : ISpawnerChickenListener, ISpawnerChickenProvider
{
    private readonly SpawnerChickenModel _model;
    private readonly SpawnerChickenView _view;

    public SpawnerChickenPresenter(SpawnerChickenModel model, SpawnerChickenView view)
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
        _view.OnSetSpawnSound += _model.SpawnSound;

        _model.OnSpawnChicken += _view.SetTypes;
    }

    private void DeactivateEvents()
    {
        _view.OnSetSpawnSound -= _model.SpawnSound;

        _model.OnSpawnChicken -= _view.SetTypes;
    }

    #region Output

    public event Action<List<IChickenUnit>> OnSpawnChickens
    {
        add => _view.OnSpawnChickens += value;
        remove => _view.OnSpawnChickens -= value;
    }

    #endregion

    #region Input

    public void SpawnChickens() => _model.SpawnChickens();

    #endregion
}

public interface ISpawnerChickenListener
{
    public event Action<List<IChickenUnit>> OnSpawnChickens;
}

public interface ISpawnerChickenProvider
{
    public void SpawnChickens();
}
