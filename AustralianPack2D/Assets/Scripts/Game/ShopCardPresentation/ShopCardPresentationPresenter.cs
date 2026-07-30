using System;

public class ShopCardPresentationPresenter : IShopCardPresentationProvider, IShopCardPresentationListener
{
    private readonly ShopCardPresentationModel _model;
    private readonly ShopCardPresentationView _view;

    public ShopCardPresentationPresenter(ShopCardPresentationModel model, ShopCardPresentationView view)
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
        _view.OnClickCard += _model.ClickCard;

        _model.OnClickCard += ClickCard;
        _model.OnBuyCards += _view.SetCards;
    }

    private void DeactivateEvents()
    {
        _view.OnClickCard -= _model.ClickCard;

        _model.OnClickCard -= ClickCard;
        _model.OnBuyCards -= _view.SetCards;
    }

    private void ClickCard(CardOpenResult card)
    {
        _view.CardPresentation(card.Card.Sprite);

        OnClickCard?.Invoke();
    }

    #region Output

    public event Action<int> OnGetCountUniqueCards
    {
        add => _model.OnGetCountUniqueCards += value;
        remove => _model.OnGetCountUniqueCards -= value;
    }

    public event Action OnClickCard;

    #endregion

    #region Input

    public bool IsHasDuplicates => _view.IsHasDuplicates();
    public void Show(float time) => _view.Show(time);
    public void Hide() => _view.Hide();
    public void ShowDuplicates() => _view.ShowDuplicates();

    #endregion
}

public interface IShopCardPresentationProvider
{
    public bool IsHasDuplicates { get; }
    public void Show(float time);
    public void Hide();
    public void ShowDuplicates();
}

public interface IShopCardPresentationListener
{
    public event Action<int> OnGetCountUniqueCards;
    public event Action OnClickCard;
}
