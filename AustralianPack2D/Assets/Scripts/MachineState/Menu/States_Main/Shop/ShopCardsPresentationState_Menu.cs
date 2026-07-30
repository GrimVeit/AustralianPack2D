using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCardsPresentationState_Menu : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IShopCardPresentationProvider _cardPresentationProvider;
    private readonly IShopCardPresentationListener _cardPresentationListener;

    public ShopCardsPresentationState_Menu(IStateMachineProvider stateMachineProvider, UIMainMenuRoot sceneRoot, IShopCardPresentationProvider cardPresentationProvider, IShopCardPresentationListener cardPresentationListener)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _cardPresentationProvider = cardPresentationProvider;
        _cardPresentationListener = cardPresentationListener;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SHOP CARDS PRESENTATION / MENU</color>");

        _sceneRoot.OnClickToExit_ShopHeader += ChangeStateToChoosePack;
        _sceneRoot.OnClickToAlbum_ShopOpenPackFooter += ChangeStateToAlbum;
        _cardPresentationListener.OnClickCard += ChangeStateToCardPresentation;

        _sceneRoot.OpenShopHeaderPanel();
        _sceneRoot.OpenShopOpenPackPanel();
        _sceneRoot.OpenShopOpenFooterPackPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_ShopHeader -= ChangeStateToChoosePack;
        _sceneRoot.OnClickToAlbum_ShopOpenPackFooter -= ChangeStateToAlbum;
        _cardPresentationListener.OnClickCard -= ChangeStateToCardPresentation;

        _sceneRoot.CloseShopOpenFooterPackPanel();
        _sceneRoot.CloseShopOpenPackPanel();
    }

    private void ChangeStateToAlbum()
    {
        _sceneRoot.CloseShopHeaderPanel();
        _sceneRoot.CloseBackgroundPanel_Green();
        _cardPresentationProvider.Hide();

        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<AlbumState_Menu>());
    }

    private void ChangeStateToChoosePack()
    {
        _sceneRoot.CloseBackgroundPanel_Green();
        _cardPresentationProvider.Hide();

        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<ShopChoosePackState_Menu>());
    }

    private void ChangeStateToCardPresentation()
    {
        _sceneRoot.CloseShopHeaderPanel();

        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<ShopCardPresentationState_Menu>());
    }
}
