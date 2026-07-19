using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadersState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private UIMainMenuRoot _sceneRoot;

    public LeadersState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - LEADERS / MENU</color>");

        _sceneRoot.OnClickToExit_LeadersHeader += ChangeStateToMain;

        _sceneRoot.OpenLeadersHeaderPanel();
        _sceneRoot.OpenLeadersMiddlePanel();
        _sceneRoot.OpenBackgroundPanel_Green();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_LeadersHeader -= ChangeStateToMain;

        _sceneRoot.CloseLeadersHeaderPanel();
        _sceneRoot.CloseLeadersMiddlePanel();
        _sceneRoot.CloseBackgroundPanel_Green();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }
}
