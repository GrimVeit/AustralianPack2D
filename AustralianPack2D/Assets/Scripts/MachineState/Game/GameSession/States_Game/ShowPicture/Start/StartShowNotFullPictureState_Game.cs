using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShowNotFullPictureState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public StartShowNotFullPictureState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToExit_CARDSHEADER += ChangeStateCardsType;

        _sceneRoot.OpenNotFullPicturePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_CARDSHEADER -= ChangeStateCardsType;

        _sceneRoot.CloseNotFullPicturePanel();
    }

    private void ChangeStateCardsType()
    {
        _machineProvider.EnterState(_machineProvider.GetState<StartCardsTypeState_Game>());
    }
}
