public class CardUniqueCounterPresenter
{
    private readonly CardUniqueCounterModel _model;
    private readonly CardUniqueCounterView _view;

    public CardUniqueCounterPresenter(CardUniqueCounterModel model, CardUniqueCounterView view)
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
        _model.OnGetCountUniqueCards += _view.SetCount;
    }

    private void DeactivateEvents()
    {
        _model.OnGetCountUniqueCards -= _view.SetCount;
    }
}
