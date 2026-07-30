using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopChoosePackState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly ICardBoxBuyVisualListener _cardBoxBuyVisualListener;

    public ShopChoosePackState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, ICardBoxBuyVisualListener cardBoxBuyVisualListener)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _cardBoxBuyVisualListener = cardBoxBuyVisualListener;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SHOP CHOOSE PACK / MENU</color>");

        _sceneRoot.OnClickToExit_ShopHeader += ChangeStateToShop;
        _cardBoxBuyVisualListener.OnCardBoxBuy += ChangeStateToShopOpenPack;

        _sceneRoot.OpenShopChoosePackPanel();
        _sceneRoot.OpenShopBalancePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_ShopHeader -= ChangeStateToShop;
        _cardBoxBuyVisualListener.OnCardBoxBuy -= ChangeStateToShopOpenPack;

        _sceneRoot.CloseShopChoosePackPanel();
        _sceneRoot.CloseShopBalancePanel();
    }

    private void ChangeStateToShop()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShopState_Menu>());
    }

    private void ChangeStateToShopOpenPack()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShopOpenPackState_Menu>());
    }
}
