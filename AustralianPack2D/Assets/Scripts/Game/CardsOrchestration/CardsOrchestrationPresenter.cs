using System;

public class CardsOrchestrationPresenter : ICardsOrchectrationListener
{
    private readonly CardsOrchestrationModel _model;

    public CardsOrchestrationPresenter(CardsOrchestrationModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Output

    public event Action OnAddMove
    {
        add => _model.OnAddMove += value;
        remove => _model.OnAddMove -= value;
    }

    public event Action OnAddMatch
    {
        add => _model.OnAddMatch += value;
        remove => _model.OnAddMatch -= value;
    }

    #endregion
}

public interface ICardsOrchectrationListener
{
    public event Action OnAddMove;
    public event Action OnAddMatch;
}
