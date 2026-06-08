using System;

public class SlotMachinePresenter : ISlotMachineListener, ISlotMachineProvider
{
    private readonly SlotMachineModel _model;
    private readonly SlotMachineView _view;

    public SlotMachinePresenter(SlotMachineModel model, SlotMachineView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        _view.Initialize();

        ActivateInputEvents();
    }

    public void Dispose()
    {
        DeactivateInputEvents();

        _view.Dispose();
    }

    private void ActivateInputEvents()
    {
        _view.OnStopSpinSlot += _model.StopSpinSlot;
        _view.OnClickSpin += _model.ActivateMachine;
        _view.OnWheelSpeed += _model.WheelSpeed;

        _model.OnActivateMachine += _view.ActivateMachine;
        _model.OnActivateMachine += _view.DeactivateEffectButton;
    }

    private void DeactivateInputEvents()
    {
        _view.OnStopSpinSlot -= _model.StopSpinSlot;
        _view.OnClickSpin -= _model.ActivateMachine;
        _view.OnWheelSpeed -= _model.WheelSpeed;

        _model.OnActivateMachine -= _view.ActivateMachine;
        _model.OnActivateMachine -= _view.DeactivateEffectButton;
    }

    #region Output

    public event Action<int> OnGetLocation
    {
        add => _model.OnGetLocation += value;
        remove => _model.OnGetLocation -= value;
    }

    public event Action OnEnd
    {
        add => _model.OnEnd += value;
        remove => _model.OnEnd -= value;
    }

    #endregion

    #region Input

    public void ActivateSpinButton()
    {
        _view.ActivateEffectButton();
    }

    #endregion
}

public interface ISlotMachineListener
{
    public event Action<int> OnGetLocation;

    public event Action OnEnd;
}

public interface ISlotMachineProvider
{
    public void ActivateSpinButton();
}
