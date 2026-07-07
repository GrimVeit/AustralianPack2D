using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public ShopState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SHOP / MENU</color>");

        _sceneRoot.OnClickToExit_ShopHeader += ChangeStateToMain;
        _sceneRoot.OnClickToCover_ShopMiddle += ChangeStateToShopCover;
        _sceneRoot.OnClickToCardPack_ShopMiddle += ChangeStateToShopChoosePack;

        _sceneRoot.OpenShopHeaderPanel();
        _sceneRoot.OpenShopMiddlePanel();
        _sceneRoot.OpenBackgroundPanel_Shop();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_ShopHeader -= ChangeStateToMain;
        _sceneRoot.OnClickToCover_ShopMiddle -= ChangeStateToShopCover;
        _sceneRoot.OnClickToCardPack_ShopMiddle -= ChangeStateToShopChoosePack;

        _sceneRoot.CloseShopMiddlePanel();
    }

    private void ChangeStateToMain()
    {
        _sceneRoot.CloseShopHeaderPanel();
        _sceneRoot.CloseBackgroundPanel_Shop();

        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }


    private void ChangeStateToShopCover()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShopCoverState_Menu>());
    }

    private void ChangeStateToShopChoosePack()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShopChoosePackState_Menu>());
    }
}
