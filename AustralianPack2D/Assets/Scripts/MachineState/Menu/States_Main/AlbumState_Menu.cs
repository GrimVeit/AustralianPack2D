using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private UIMainMenuRoot _sceneRoot;

    public AlbumState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - ALBUM / MENU</color>");

        _sceneRoot.OnClickToExit_AlbumHeader += ChangeStateToMain;

        _sceneRoot.OpenAlbumHeaderPanel();
        _sceneRoot.OpenAlbumMiddlePanel();
        _sceneRoot.OpenBackgroundPanel_Album();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_AlbumHeader -= ChangeStateToMain;

        _sceneRoot.CloseAlbumHeaderPanel();
        _sceneRoot.CloseAlbumMiddlePanel();
        _sceneRoot.CloseBackgroundPanel_Album();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }
}
