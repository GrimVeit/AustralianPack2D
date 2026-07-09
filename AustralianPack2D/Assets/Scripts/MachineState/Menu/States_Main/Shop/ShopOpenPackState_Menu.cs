using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpenPackState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly ICardBoxProvider _cardBoxProvider;
    private readonly ICardBoxListener _cardBoxListener;

    private bool _isReturn;

    private IEnumerator timer;

    public ShopOpenPackState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, ICardBoxProvider cardBoxProvider, ICardBoxListener cardBoxListener)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _cardBoxProvider = cardBoxProvider;
        _cardBoxListener = cardBoxListener;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SHOP OPEN PACK / MENU</color>");

        if (timer != null) Coroutines.Stop(timer);

        _cardBoxListener.OnEndOpen += Return;

        _sceneRoot.CloseShopHeaderPanel();
        _sceneRoot.OpenShopOpenPackPanel();

        _isReturn = false;

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _cardBoxListener.OnEndOpen -= Return;

        _sceneRoot.CloseShopOpenPackPanel();
    }

    private IEnumerator Timer()
    {
        _cardBoxProvider.Hide();
        _cardBoxProvider.Show(0.7f);

        yield return new WaitForSeconds(1.7f);

        _cardBoxProvider.ActivateOpen();

        yield return new WaitUntil(() => _isReturn);

        yield return new WaitForSeconds(2);

        //ChangeStateToShopChoosePack();
    }

    private void Return()
    {
        _isReturn = true;
    }

    private void ChangeStateToShopChoosePack()
    {
        _cardBoxProvider.Hide();
        _sceneRoot.OpenShopHeaderPanel();

        _machineProvider.EnterState(_machineProvider.GetState<ShopChoosePackState_Menu>());
    }
}
