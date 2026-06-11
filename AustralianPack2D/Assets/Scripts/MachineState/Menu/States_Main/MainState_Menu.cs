using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public MainState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - MAIN / MENU</color>");

        _sceneRoot.OnClickToLevel_MainMiddle += ChangeStateToLevel;
        _sceneRoot.OnClickToSettings_MainMiddle += ChangeStateToSettings;
        _sceneRoot.OnClickToShop_MainMiddle += ChangeStateToShop;
        _sceneRoot.OnClickToAlbum_MainMiddle += ChangeStateToAlbum;

        _sceneRoot.OpenMainHeaderPanel();
        _sceneRoot.OpenMainMiddlePanel();
        _sceneRoot.OpenPlayFooterPanel();
        _sceneRoot.OpenBackgroundPanel_Main();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToLevel_MainMiddle -= ChangeStateToLevel;
        _sceneRoot.OnClickToSettings_MainMiddle -= ChangeStateToSettings;
        _sceneRoot.OnClickToShop_MainMiddle -= ChangeStateToShop;
        _sceneRoot.OnClickToAlbum_MainMiddle -= ChangeStateToAlbum;

        _sceneRoot.CloseMainHeaderPanel();
        _sceneRoot.CloseMainMiddlePanel();
        _sceneRoot.CloseBackgroundPanel_Main();
    }

    private void ChangeStateToLevel()
    {
        _machineProvider.EnterState(_machineProvider.GetState<LevelState_Menu>());
    }

    private void ChangeStateToSettings()
    {
        _sceneRoot.ClosePlayFooterPanel();

        _machineProvider.EnterState(_machineProvider.GetState<SettingsState_Menu>());
    }

    private void ChangeStateToShop()
    {
        _sceneRoot.ClosePlayFooterPanel();

        _machineProvider.EnterState(_machineProvider.GetState<ShopState_Menu>());
    }

    private void ChangeStateToAlbum()
    {
        _sceneRoot.ClosePlayFooterPanel();

        _machineProvider.EnterState(_machineProvider.GetState<AlbumState_Menu>());
    }
}
