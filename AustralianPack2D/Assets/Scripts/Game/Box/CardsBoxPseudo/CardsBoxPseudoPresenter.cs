using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsBoxPseudoPresenter : ICardsBoxPseudoListener, ICardsBoxPseudoProvider
{
    private readonly CardsBoxPseudoModel _model;
    private readonly CardsBoxPseudoView _view;

    public CardsBoxPseudoPresenter(CardsBoxPseudoModel model, CardsBoxPseudoView view)
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
        _model.OnSetDesign += _view.SetDesign;
    }

    private void DeactivateEvents()
    {
        _model.OnSetDesign -= _view.SetDesign;
    }

    #region Output

    public event Action OnEndMove
    {
        add => _view.OnEndMove += value;
        remove => _view.OnEndMove -= value;
    }

    public event Action OnEndRotate
    {
        add => _view.OnEndRotate += value;
        remove => _view.OnEndRotate -= value;
    }

    #endregion

    #region Input

    public void Show() => _view.Show();
    public void Hide() => _view.Hide();
    public void MoveToShow(float time) => _view.MoveToShow(time);
    public void ShowRotate(float time) => _view.ShowRotate(time);

    #endregion
}

public interface ICardsBoxPseudoListener
{
    public event Action OnEndMove;
    public event Action OnEndRotate;
}

public interface ICardsBoxPseudoProvider
{
    public void Show();
    public void Hide();
    public void MoveToShow(float time);
    public void ShowRotate(float time);
}
