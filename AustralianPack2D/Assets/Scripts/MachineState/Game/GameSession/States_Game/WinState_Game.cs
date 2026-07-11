using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIGameRoot _sceneRoot;

    public WinState_Game(IStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OpenWinPanel();
    }

    public void ExitState()
    {
        _sceneRoot.CloseWinPanel();
    }
}
