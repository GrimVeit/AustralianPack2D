using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumTableState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public AlbumTableState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToExit_AlbumHeader += ChangeStateToAlbum;

        _sceneRoot.OpenAlbumTablePanel();
        _sceneRoot.OpenAlbumTableFooterPanel();
        //_sceneRoot.OpenBackgroundPanel_Green();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_AlbumHeader -= ChangeStateToAlbum;

        _sceneRoot.CloseAlbumTablePanel();
        _sceneRoot.CloseAlbumTableFooterPanel();
        //_sceneRoot.CloseBackgroundPanel_Green();
    }

    private void ChangeStateToAlbum()
    {
        _machineProvider.EnterState(_machineProvider.GetState<AlbumState_Menu>());
    }
}
