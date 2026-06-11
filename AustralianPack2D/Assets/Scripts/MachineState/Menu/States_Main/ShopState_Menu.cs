using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private UIMainMenuRoot _sceneRoot;

    public ShopState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SHOP / MENU</color>");

        _sceneRoot.OnClickToExit_ShopHeader += ChangeStateToMain;

        _sceneRoot.OpenShopHeaderPanel();
        _sceneRoot.OpenShopMiddlePanel();
        _sceneRoot.OpenBackgroundPanel_Shop();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_ShopHeader -= ChangeStateToMain;

        _sceneRoot.CloseShopHeaderPanel();
        _sceneRoot.CloseShopMiddlePanel();
        _sceneRoot.CloseBackgroundPanel_Shop();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }
}
