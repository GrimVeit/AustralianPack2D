using System;
using UnityEngine;

public class ChickenUnitPresenter : IChickenUnit
{
    private readonly ChickenUnitModel _model;
    private readonly ChickenUnitView _view;

    private readonly ChickenStateMachine _stateMachine;

    public ChickenUnitPresenter(ChickenUnitModel model, ISoundProvider soundProvider, ChickenUnitView view)
    {
        _model = model;
        _view = view;

        _stateMachine = new ChickenStateMachine(_model, soundProvider);
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
        _stateMachine.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
        _stateMachine.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnEndMove += _model.EndMove;
        _model.OnEndMove += EndMove;

        _model.OnShow += _view.Show;
        _model.OnHide += _view.HideDestroy;
        _model.OnActivateAnimation += _view.ActivateAnimation;

        _model.OnStartMove += _view.StartMove;
        _model.OnStopMove += _view.StopMove;
        _model.OnSetSpeed += _view.SetSpeed;
        _model.OnSetSpeed_Smooth += _view.SetSpeed;


        _model.OnEventsGame_Auto_Activate += _view.Auto.Activate;
        _model.OnEventsGame_Auto_Clear += _view.Auto.Clear;

        _model.OnEventsGame_UFO_Activate += _view.UFO.Activate;
        _model.OnEventsGame_UFO_Clear += _view.UFO.Clear;

        _model.OnEventsGame_Tornado_Activate += _view.Tornado.Activate;
        _model.OnEventsGame_Tornado_Clear += _view.Tornado.Clear;

        _model.OnEventsGame_Ghost_Activate += _view.Ghost.Activate;
        _model.OnEventsGame_Ghost_Clear += _view.Ghost.Clear;

        _model.OnEventsGame_Hunter_Activate += _view.Hunter.Activate;
        _model.OnEventsGame_Hunter_Clear += _view.Hunter.Clear;

        _model.OnEventsGame_Pigeon_ActivateStart += _view.Pigeon.ActivateStart;
        _model.OnEventsGame_Pigeon_ActivateEnd += _view.Pigeon.ActivateEnd;
        _model.OnEventsGame_Pigeon_Clear += _view.Pigeon.Clear;
    }

    private void DeactivateEvents()
    {
        _view.OnEndMove -= _model.EndMove;
        _model.OnEndMove -= EndMove;

        _model.OnShow -= _view.Show;
        _model.OnHide -= _view.HideDestroy;
        _model.OnActivateAnimation -= _view.ActivateAnimation;

        _model.OnStartMove -= _view.StartMove;
        _model.OnStopMove -= _view.StopMove;
        _model.OnSetSpeed -= _view.SetSpeed;
        _model.OnSetSpeed_Smooth -= _view.SetSpeed;


        _model.OnEventsGame_Auto_Activate -= _view.Auto.Activate;
        _model.OnEventsGame_Auto_Clear -= _view.Auto.Clear;

        _model.OnEventsGame_UFO_Activate -= _view.UFO.Activate;
        _model.OnEventsGame_UFO_Clear -= _view.UFO.Clear;

        _model.OnEventsGame_Tornado_Activate -= _view.Tornado.Activate;
        _model.OnEventsGame_Tornado_Clear -= _view.Tornado.Clear;

        _model.OnEventsGame_Ghost_Activate -= _view.Ghost.Activate;
        _model.OnEventsGame_Ghost_Clear -= _view.Ghost.Clear;

        _model.OnEventsGame_Hunter_Activate -= _view.Hunter.Activate;
        _model.OnEventsGame_Hunter_Clear -= _view.Hunter.Clear;

        _model.OnEventsGame_Pigeon_ActivateStart -= _view.Pigeon.ActivateStart;
        _model.OnEventsGame_Pigeon_ActivateEnd -= _view.Pigeon.ActivateEnd;
        _model.OnEventsGame_Pigeon_Clear -= _view.Pigeon.Clear;
    }

    #region Input

    public ChickenType Type => _model.Type;
    public Vector3 LocalPosition => _view.LocalPosition;

    public void ActivateGoodState() => _stateMachine.ActivateGoodState();
    public void ActivateBadState() => _stateMachine.ActivateBadState();
    public void SetRun() => _stateMachine.SetRun();
    public void SetIdle() => _stateMachine.SetIdle();

    #endregion

    #region Output

    public event Action<IChickenUnit> OnEndMove;

    private void EndMove()
    {
        OnEndMove?.Invoke(this);
    }

    #endregion
}

public interface IChickenUnit
{
    public ChickenType Type { get; }

    public void Initialize();
    public void Dispose();

    public void ActivateGoodState();
    public void ActivateBadState();
    public void SetRun();
    public void SetIdle();

    public Vector3 LocalPosition { get; }

    public event Action<IChickenUnit> OnEndMove;
}
