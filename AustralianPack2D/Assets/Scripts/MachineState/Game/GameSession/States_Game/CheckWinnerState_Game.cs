using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinnerState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly IChickenBattleListener _chickenBattleListener;
    private readonly IChickenBattleProvider _chickenBattleProvider;

    private readonly IChooseChickenProvider _chooseChickenProvider;

    private IEnumerator timer;

    public CheckWinnerState_Game(IStateMachineProvider stateMachineProvider, IChickenBattleListener chickenBattleListener, IChickenBattleProvider chickenBattleProvider, IChooseChickenProvider chooseChickenProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _chickenBattleListener = chickenBattleListener;
        _chickenBattleProvider = chickenBattleProvider;

        _chooseChickenProvider = chooseChickenProvider;
    }

    public void EnterState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _chickenBattleListener.OnWin += ChangeStateToStartWin;
        _chickenBattleListener.OnLose += ChangeStateToStartLose;

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _chickenBattleListener.OnWin -= ChangeStateToStartWin;
        _chickenBattleListener.OnLose -= ChangeStateToStartLose;
    }

    private IEnumerator Timer()
    {
        _chooseChickenProvider.HideAll();

        yield return new WaitForSeconds(1f);

        _chickenBattleProvider.CheckWinner();
    }

    private void ChangeStateToStartWin()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<StartWinState_Game>());
    }

    private void ChangeStateToStartLose()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<StartLoseState_Game>());
    }
}
