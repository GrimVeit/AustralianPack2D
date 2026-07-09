using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCardsPresentationState_Menu : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly ICardPresentationProvider _cardPresentationProvider;

    public ShopCardsPresentationState_Menu(IStateMachineProvider stateMachineProvider, UIMainMenuRoot sceneRoot, ICardPresentationProvider cardPresentationProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _cardPresentationProvider = cardPresentationProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SHOP CARDS PRESENTATION / MENU</color>");

        _sceneRoot.OnClickToExit_ShopHeader += ChangeStateToChoosePack;
        _sceneRoot.OnClickToAlbum_ShopOpenPackFooter += ChangeStateToAlbum;

        _sceneRoot.OpenShopHeaderPanel();
        _sceneRoot.OpenShopOpenFooterPackPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_ShopHeader -= ChangeStateToChoosePack;
        _sceneRoot.OnClickToAlbum_ShopOpenPackFooter -= ChangeStateToAlbum;

        _sceneRoot.CloseShopOpenFooterPackPanel();
    }

    private void ChangeStateToAlbum()
    {
        _sceneRoot.CloseShopHeaderPanel();
        _sceneRoot.CloseBackgroundPanel_Green();
        _sceneRoot.CloseShopOpenPackPanel();
        _sceneRoot.CloseBackgroundPanel_Green();
        _cardPresentationProvider.Hide();

        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<AlbumState_Menu>());
    }

    private void ChangeStateToChoosePack()
    {
        _sceneRoot.CloseBackgroundPanel_Green();
        _sceneRoot.CloseShopOpenPackPanel();
        _sceneRoot.CloseBackgroundPanel_Green();
        _cardPresentationProvider.Hide();

        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<ShopChoosePackState_Menu>());
    }
}
