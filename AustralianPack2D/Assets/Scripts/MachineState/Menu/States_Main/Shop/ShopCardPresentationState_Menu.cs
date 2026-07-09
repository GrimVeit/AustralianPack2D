using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCardPresentationState_Menu : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public ShopCardPresentationState_Menu(IStateMachineProvider stateMachineProvider, UIMainMenuRoot sceneRoot)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBack_ShopCardPresentation += ChangeStateShopCardsPresentation;

        _sceneRoot.OpenShopCardPresentationPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_ShopCardPresentation -= ChangeStateShopCardsPresentation;

        _sceneRoot.CloseShopCardPresentationPanel();
    }

    private void ChangeStateShopCardsPresentation()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<ShopCardsPresentationState_Menu>());
    }
}
