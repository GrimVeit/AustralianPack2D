using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLocationState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IMaskEffectProvider _maskEffectProvider;
    private readonly ISlotMachineListener _slotMachineListener;
    private readonly ISlotMachineProvider _slotMachineProvider;

    public ChooseLocationState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot, IMaskEffectProvider maskEffectProvider, ISlotMachineListener slotMachineListener, ISlotMachineProvider slotMachineProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _maskEffectProvider = maskEffectProvider;
        _slotMachineListener = slotMachineListener;
        _slotMachineProvider = slotMachineProvider;
    }

    public void EnterState()
    {
        _slotMachineListener.OnEnd += ChangeStateToStartGameRun;

        _maskEffectProvider.Play("Location", () =>
        {
            _maskEffectProvider.Stop("Play");
            _sceneRoot.ClosePlayVideoPanel();
        });

        _sceneRoot.OpenBackgroundBrownPanel();
        _sceneRoot.OpenChooseLocationPanel();
        _slotMachineProvider.ActivateSpinButton();
    }

    public void ExitState()
    {
        _slotMachineListener.OnEnd -= ChangeStateToStartGameRun;

        _maskEffectProvider.Stop("Location");
    }

    private void ChangeStateToStartGameRun()
    {
        _machineProvider.EnterState(_machineProvider.GetState<StartGameRunState_Game>());
    }
}
