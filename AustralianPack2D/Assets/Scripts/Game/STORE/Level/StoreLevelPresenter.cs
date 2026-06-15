using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLevelPresenter : IStoreLevelListener, IStoreLevelProvider, IStoreLevelInfo
{
    private readonly StoreLevelModel _model;

    public StoreLevelPresenter(StoreLevelModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Output

    public event Action<GameLevel> OnChangeLevel
    {
        add => _model.OnChangeLevel += value;
        remove => _model.OnChangeLevel -= value;
    }

    public GameLevel GameLevel => _model.Level;

    #endregion

    #region Input

    public void SetLevel(GameLevel level) => _model.SetLevel(level);

    #endregion
}

public interface IStoreLevelListener
{
    public event Action<GameLevel> OnChangeLevel;
}

public interface IStoreLevelProvider
{
    public void SetLevel(GameLevel level);
}

public interface IStoreLevelInfo
{
    public GameLevel GameLevel { get; }
}
