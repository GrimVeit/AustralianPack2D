using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopChoosePackState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public ShopChoosePackState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToExit_ShopHeader += ChangeStateToShop;

        _sceneRoot.OpenShopChoosePackPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_ShopHeader -= ChangeStateToShop;

        _sceneRoot.CloseShopChoosePackPanel();
    }

    private void ChangeStateToShop()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShopState_Menu>());
    }
}
