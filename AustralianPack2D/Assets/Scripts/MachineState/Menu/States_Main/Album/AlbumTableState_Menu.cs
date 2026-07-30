using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumTableState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly ICardVisualListener _cardVisualListener;

    public AlbumTableState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, ICardVisualListener cardVisualListener)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _cardVisualListener = cardVisualListener;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToExit_AlbumHeader += ChangeStateToAlbum;
        _cardVisualListener.OnClickCard += ChangeStateToAlbumCardPresentation;

        _sceneRoot.OpenAlbumTablePanel();
        _sceneRoot.OpenAlbumTableFooterPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_AlbumHeader -= ChangeStateToAlbum;
        _cardVisualListener.OnClickCard -= ChangeStateToAlbumCardPresentation;

        _sceneRoot.CloseAlbumTablePanel();
        _sceneRoot.CloseAlbumTableFooterPanel();
    }

    private void ChangeStateToAlbum()
    {
        _machineProvider.EnterState(_machineProvider.GetState<AlbumState_Menu>());
    }

    private void ChangeStateToAlbumCardPresentation()
    {
        _machineProvider.EnterState(_machineProvider.GetState<AlbumCardPresentationState_Menu>());
    }
}
