using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuyBoxState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly IChooseBuyBoxProvider _chooseBuyBoxProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IMoneyProvider _moneyProvider;
    private readonly ISoundProvider _soundProvider;

    public ChooseBuyBoxState_Game(IStateMachineProvider machineProvider, IChooseBuyBoxProvider chooseBuyBoxProvider, UIGameRoot sceneRoot, IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        _machineProvider = machineProvider;
        _chooseBuyBoxProvider = chooseBuyBoxProvider;
        _sceneRoot = sceneRoot;
        _moneyProvider = moneyProvider;
        _soundProvider = soundProvider;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBox_COSTBOX += ChangeStateToBuyBox;
        _sceneRoot.OnClickToExit_COSTBOX += ChangeStateToWin;

        _sceneRoot.OpenBackgroundBrownPanel();
        _sceneRoot.OpenCostBoxPanel();
        _chooseBuyBoxProvider.ShowAll();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBox_COSTBOX -= ChangeStateToBuyBox;
        _sceneRoot.OnClickToExit_COSTBOX -= ChangeStateToWin;

        _sceneRoot.CloseCostBoxPanel();
        _chooseBuyBoxProvider.HideAll();
    }

    private void ChangeStateToBuyBox()
    {
        if (_moneyProvider.CanAfford(5))
        {
            _soundProvider.PlayOneShot("Buy");
            _moneyProvider.SendMoney(-5);
            _machineProvider.EnterState(_machineProvider.GetState<BuyBoxState_Game>());
        }
        else
        {
            _soundProvider.PlayOneShot("Error");
        }
    }

    private void ChangeStateToWin()
    {
        _machineProvider.EnterState(_machineProvider.GetState<WinState_Game>());
    }
}
