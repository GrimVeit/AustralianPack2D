using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public SettingsState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToExit_SETTINGS += ChangeStateToPlay;

        _sceneRoot.OpenBackgroundBarnPanel();
        _sceneRoot.OpenSettingsPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_SETTINGS -= ChangeStateToPlay;


        _sceneRoot.CloseBackgroundBarnPanel();
        _sceneRoot.CloseSettingsPanel();
    }

    private void ChangeStateToPlay()
    {
        _machineProvider.EnterState(_machineProvider.GetState<PlayVideoState_Game>());
    }
}
