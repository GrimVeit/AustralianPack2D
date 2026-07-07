using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoxPresenter : ICardBoxProvider, ICardBoxListener
{
    private readonly CardBoxView _view;

    public CardBoxPresenter(CardBoxView view)
    {
        _view = view;
    }

    public void Initialize()
    {
        _view.Initialize();
    }

    public void Dispose()
    {
        _view.Dispose();
    }

    #region Output

    public event Action OnEndOpen
    {
        add => _view.OnEndOpen += value;
        remove => _view.OnEndOpen -= value;
    }

    #endregion

    #region Input

    public void Show() => _view.Show();

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
    public void Show();
    public void Hide();
}
