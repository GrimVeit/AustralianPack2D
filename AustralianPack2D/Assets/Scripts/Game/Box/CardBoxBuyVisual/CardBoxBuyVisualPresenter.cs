using System;

public class CardBoxBuyVisualPresenter : ICardBoxBuyVisualListener
{
    private readonly CardBoxBuyVisualModel _model;
    private readonly CardBoxBuyVisualView _view;

    public CardBoxBuyVisualPresenter(CardBoxBuyVisualModel model, CardBoxBuyVisualView view)
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
        _view.OnClickBuy += _model.BuyBox;
    }

    private void DeactivateEvents()
    {
        _view.OnClickBuy -= _model.BuyBox;
    }

    #region Output

    public event Action OnCardBoxBuy
    {
        add => _model.OnCardBoxBuy += value;
        remove => _model.OnCardBoxBuy -= value;
    }

    #endregion
}

public interface ICardBoxBuyVisualListener
{
    public event Action OnCardBoxBuy;
}
