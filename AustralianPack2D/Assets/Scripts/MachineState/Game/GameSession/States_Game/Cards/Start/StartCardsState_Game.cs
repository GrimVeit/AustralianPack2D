using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCardsState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IVisualChickenPictureListener _visualChickenPictureListener;
    private readonly ICountChickenPictureProvider _countChickenPictureProvider;

    public StartCardsState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot, IVisualChickenPictureListener visualChickenPictureListener, ICountChickenPictureProvider countChickenPictureProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _visualChickenPictureListener = visualChickenPictureListener;
        _countChickenPictureProvider = countChickenPictureProvider;
    }

    public void EnterState()
    {
        _visualChickenPictureListener.OnSelectType += ChangeStateToStartCardsType;
        _sceneRoot.OnClickToExit_CARDSHEADER += ChangeStateToPlayVideo;
        _sceneRoot.OnClickToMenu_CARDSHEADER += ChangeStateToPlayVideo;

        _countChickenPictureProvider.ShowCount();

        _sceneRoot.OpenBackgroundBrownPanel();
        _sceneRoot.OpenPiecesPanel();
        _sceneRoot.OpenCardsHeaderPanel();
        _sceneRoot.OpenCardsPanel();
    }

    public void ExitState()
    {
        _visualChickenPictureListener.OnSelectType -= ChangeStateToStartCardsType;
        _sceneRoot.OnClickToExit_CARDSHEADER -= ChangeStateToPlayVideo;
        _sceneRoot.OnClickToMenu_CARDSHEADER -= ChangeStateToPlayVideo;

        _sceneRoot.ClosePiecesPanel();
        _sceneRoot.CloseCardsPanel();
    }

    private void ChangeStateToPlayVideo()
    {
        _machineProvider.EnterState(_machineProvider.GetState<PlayVideoState_Game>());

        _sceneRoot.CloseCardsHeaderPanel();
        _sceneRoot.CloseBackgroundBrownPanel();
    }

    private void ChangeStateToStartCardsType()
    {
        _machineProvider.EnterState(_machineProvider.GetState<StartCardsTypeState_Game>());
    }
}
