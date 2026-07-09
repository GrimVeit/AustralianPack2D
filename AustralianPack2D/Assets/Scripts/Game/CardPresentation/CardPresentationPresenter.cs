using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPresentationPresenter : ICardPresentationProvider
{
    private readonly CardPresentationModel _model;
    private readonly CardPresentationView _view;

    public CardPresentationPresenter(CardPresentationModel model, CardPresentationView view)
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
        _model.OnBuyCards += _view.SetCards;
    }

    private void DeactivateEvents()
    {
        _model.OnBuyCards -= _view.SetCards;
    }

    #region Input

    public void Show(float time) => _view.Show(time);
    public void Hide() => _view.Hide();
    public void ShowDuplicates() => _view.ShowDuplicates();

    #endregion
}

public interface ICardPresentationProvider
{
    public void Show(float time);
    public void Hide();
    public void ShowDuplicates();
}
