using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseChickenState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly IChooseChickenProvider _chooseChickenProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IVisualChickenEffectProvider _visualChickenEffectProvider;

    public ChooseChickenState_Game(IStateMachineProvider machineProvider, IChooseChickenProvider chooseChickenProvider, UIGameRoot sceneRoot, IVisualChickenEffectProvider visualChickenEffectProvider)
    {
        _machineProvider = machineProvider;
        _chooseChickenProvider = chooseChickenProvider;
        _sceneRoot = sceneRoot;
        _visualChickenEffectProvider = visualChickenEffectProvider;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToChoose_CHOOSE += ChangeStateToWaitStartGame;

        _sceneRoot.OpenChoosePanel();
        _chooseChickenProvider.ActivateAll();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToChoose_CHOOSE -= ChangeStateToWaitStartGame;

        _sceneRoot.CloseChoosePanel();
        _chooseChickenProvider.DeactivateAll();
        _visualChickenEffectProvider.DeactivateVisual();
    }

    private void ChangeStateToWaitStartGame()
    {
        _machineProvider.EnterState(_machineProvider.GetState<WaitGameRunState_Game>());
    }
}
