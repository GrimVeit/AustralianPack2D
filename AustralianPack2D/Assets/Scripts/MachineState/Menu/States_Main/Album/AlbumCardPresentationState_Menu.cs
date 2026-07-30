using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumCardPresentationState_Menu : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public AlbumCardPresentationState_Menu(IStateMachineProvider stateMachineProvider, UIMainMenuRoot sceneRoot)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBack_CardPresentation += ChangeStateShopCardsPresentation;

        _sceneRoot.CloseAlbumHeaderPanel();
        _sceneRoot.OpenCardPresentationPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_CardPresentation -= ChangeStateShopCardsPresentation;

        _sceneRoot.CloseCardPresentationPanel();
        _sceneRoot.OpenAlbumHeaderPanel();
    }

    private void ChangeStateShopCardsPresentation()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<AlbumTableState_Menu>());
    }
}
