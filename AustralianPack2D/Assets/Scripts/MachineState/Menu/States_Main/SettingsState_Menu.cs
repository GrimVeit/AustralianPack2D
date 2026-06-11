using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private UIMainMenuRoot _sceneRoot;

    public SettingsState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SETTINGS / MENU</color>");

        _sceneRoot.OnClickToExit_SettingsHeader += ChangeStateToMain;

        _sceneRoot.OpenSettingsHeaderPanel();
        _sceneRoot.OpenSettingsMiddlePanel();
        _sceneRoot.OpenBackgroundPanel_Settings();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_SettingsHeader -= ChangeStateToMain;

        _sceneRoot.CloseSettingsHeaderPanel();
        _sceneRoot.CloseSettingsMiddlePanel();
        _sceneRoot.CloseBackgroundPanel_Settings();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }
}
