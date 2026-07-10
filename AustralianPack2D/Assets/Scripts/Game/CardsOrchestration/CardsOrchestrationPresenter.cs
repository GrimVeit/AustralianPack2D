using System;

public class CardsOrchestrationPresenter : ICardsOrchectrationListener, ICardsOrchectrationProvider
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

    #region Input

    public void ActivateInteractive() => _model.ActivateInteractive();
    public void DeactivateInteractive() => _model.DeactivateInteractive();
    public void ShowCards() => _model.ShowCards();
    public void HideCards() => _model.HideCards();

    #endregion
}

public interface ICardsOrchectrationListener
{
    public event Action OnAddMove;
    public event Action OnAddMatch;
}

public interface ICardsOrchectrationProvider
{
    public void ActivateInteractive();
    public void DeactivateInteractive();

    public void ShowCards();
    public void HideCards();
}
