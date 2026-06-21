using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCoverState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public ShopCoverState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToExit_ShopHeader += ChangeStateToAlbum;

        _sceneRoot.OpenShopCoverTablePanel();
        _sceneRoot.OpenShopCoverTableFooterPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_ShopHeader -= ChangeStateToAlbum;

        _sceneRoot.CloseShopCoverTablePanel();
        _sceneRoot.CloseShopCoverTableFooterPanel();
    }

    private void ChangeStateToAlbum()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShopState_Menu>());
    }
}
