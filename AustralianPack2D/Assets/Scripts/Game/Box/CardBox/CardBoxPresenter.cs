using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoxPresenter : ICardBoxProvider, ICardBoxListener
{
    private readonly CardBoxModel _model;
    private readonly CardBoxView _view;

    public CardBoxPresenter(CardBoxModel model, CardBoxView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnSetSkin += _view.SetSkin;
    }

    private void DeactivateEvents()
    {
        _model.OnSetSkin -= _view.SetSkin;
    }

    #region Output

    public event Action OnEndOpen
    {
        add => _view.OnEndOpen += value;
        remove => _view.OnEndOpen -= value;
    }

    #endregion

    #region Input

    public void Show(float time) => _view.Show(time);

    public void Hide() => _view.Hide();

    public void ActivateOpen() => _view.ActivateOpen();

    #endregion
}

public interface ICardBoxListener
{
    public event Action OnEndOpen;
}

public interface ICardBoxProvider
{
    public void ActivateOpen();
    public void Show(float time);
    public void Hide();
}
