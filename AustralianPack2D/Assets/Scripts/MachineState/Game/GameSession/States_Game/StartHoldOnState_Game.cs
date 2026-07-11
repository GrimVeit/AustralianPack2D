using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHoldOnState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private IEnumerator timer;

    public StartHoldOnState_Game(IStateMachineProvider machineProvider)
    {
        _machineProvider = machineProvider;
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);

        ChangeStateToStart();
    }

    private void ChangeStateToStart()
    {
        _machineProvider.EnterState(_machineProvider.GetState<StartState_Game>());
    }
}
