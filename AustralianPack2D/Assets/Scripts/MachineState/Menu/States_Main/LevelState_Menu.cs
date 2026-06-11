using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private UIMainMenuRoot _sceneRoot;

    public LevelState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - LEVEL / MENU</color>");

        _sceneRoot.OnClickToExit_LevelHeader += ChangeStateToMain;

        _sceneRoot.OpenLevelHeaderPanel();
        _sceneRoot.OpenLevelMiddlePanel();
        _sceneRoot.OpenPlayFooterPanel();
        _sceneRoot.OpenBackgroundPanel_Level();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_LevelHeader -= ChangeStateToMain;

        _sceneRoot.CloseLevelHeaderPanel();
        _sceneRoot.CloseLevelMiddlePanel();
        _sceneRoot.CloseBackgroundPanel_Level();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }
}
