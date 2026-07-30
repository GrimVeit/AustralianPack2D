using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisualPresenter : ICardVisualListener
{
    private readonly CardVisualModel _model;
    private readonly CardVisualView _view;

    public CardVisualPresenter(CardVisualModel model, CardVisualView view)
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
        _view.OnCardClick += _model.ClickCard;

        _model.OnOpenCard += _view.OpenCard;
        _model.OnCloseCard += _view.CloseCard;
    }

    private void DeactivateEvents()
    {
        _view.OnCardClick -= _model.ClickCard;

        _model.OnOpenCard -= _view.OpenCard;
        _model.OnCloseCard -= _view.CloseCard;
    }

    #region Output

    public event Action<Card> OnClickCard_Value
    {
        add => _model.OnClickCard_Value += value;
        remove => _model.OnClickCard_Value -= value;
    }

    public event Action OnClickCard
    {
        add => _model.OnClickCard += value;
        remove => _model.OnClickCard -= value;
    }

    #endregion
}

public interface ICardVisualListener
{
    public event Action<Card> OnClickCard_Value;
    public event Action OnClickCard;
}
