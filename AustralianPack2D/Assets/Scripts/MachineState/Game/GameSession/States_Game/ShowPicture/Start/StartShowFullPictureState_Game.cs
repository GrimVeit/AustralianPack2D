using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShowFullPictureState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public StartShowFullPictureState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToExit_FULLPICTURE += ChangeStateToStartCardsType;

        _sceneRoot.OpenFullPicturePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_FULLPICTURE -= ChangeStateToStartCardsType;

        _sceneRoot.CloseFullPicturePanel();
    }

    private void ChangeStateToStartCardsType()
    {
        _machineProvider.EnterState(_machineProvider.GetState<StartCardsTypeState_Game>());
    }
}
