using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsTypeState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IShowChickenPictureListener _showChickenPictureListener;

    public CardsTypeState_Game(IStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, IShowChickenPictureListener showChickenPictureListener)
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

        _sceneRoot.OpenCardsTypePanel();
    }

    public void ExitState()
    {
        _showChickenPictureListener.OnShowNotFullPicture -= ChangeStateToShowNotFullPicture;
        _showChickenPictureListener.OnShowFullPicture -= ChangeStateToShowFullPicture;
        _sceneRoot.OnClickToExit_CARDSHEADER -= ChangeStateToCards;
    }

    private void ChangeStateToCards()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<CardsState_Game>());

        _sceneRoot.CloseCardsTypePanel();
    }

    private void ChangeStateToShowFullPicture()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<ShowFullPictureState_Game>());
    }

    private void ChangeStateToShowNotFullPicture()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<ShowNotFullPictureState_Game>());

        _sceneRoot.CloseCardsTypePanel();
    }
}
