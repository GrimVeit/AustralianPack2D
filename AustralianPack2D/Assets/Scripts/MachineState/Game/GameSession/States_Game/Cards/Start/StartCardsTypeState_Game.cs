using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCardsTypeState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IShowChickenPictureListener _showChickenPictureListener;

    public StartCardsTypeState_Game(IStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, IShowChickenPictureListener showChickenPictureListener)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _showChickenPictureListener = showChickenPictureListener;
    }

    public void EnterState()
    {
        _showChickenPictureListener.OnShowNotFullPicture += ChangeStateToShowNotFullPicture;
        _showChickenPictureListener.OnShowFullPicture += ChangeStateToShowFullPicture;

        _sceneRoot.OnClickToExit_CARDSHEADER += ChangeStateToCards;
        _sceneRoot.OnClickToMenu_CARDSHEADER += ChangeStateToMenu;

        _sceneRoot.OpenCardsTypePanel();
    }

    public void ExitState()
    {
        _showChickenPictureListener.OnShowNotFullPicture -= ChangeStateToShowNotFullPicture;
        _showChickenPictureListener.OnShowFullPicture -= ChangeStateToShowFullPicture;

        _sceneRoot.OnClickToExit_CARDSHEADER -= ChangeStateToCards;
        _sceneRoot.OnClickToMenu_CARDSHEADER -= ChangeStateToMenu;
    }

    private void ChangeStateToCards()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<StartCardsState_Game>());

        _sceneRoot.CloseCardsTypePanel();
    }

    private void ChangeStateToMenu()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<PlayVideoState_Game>());

        _sceneRoot.CloseCardsTypePanel();
        _sceneRoot.CloseBackgroundBrownPanel();
    }

    private void ChangeStateToShowFullPicture()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<StartShowFullPictureState_Game>());
    }

    private void ChangeStateToShowNotFullPicture()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<StartShowNotFullPictureState_Game>());

        _sceneRoot.CloseCardsTypePanel();
    }
}
