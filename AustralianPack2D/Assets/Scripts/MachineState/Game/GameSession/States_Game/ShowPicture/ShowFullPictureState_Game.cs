using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFullPictureState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public ShowFullPictureState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToExit_FULLPICTURE += ChangeStateCardsType;

        _sceneRoot.OpenFullPicturePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_FULLPICTURE -= ChangeStateCardsType;

        _sceneRoot.CloseFullPicturePanel();
    }

    private void ChangeStateCardsType()
    {
        _machineProvider.EnterState(_machineProvider.GetState<CardsTypeState_Game>());
    }
}
