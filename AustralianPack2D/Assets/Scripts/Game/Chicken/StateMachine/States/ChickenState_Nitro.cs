using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenState_Nitro : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly ChickenUnitModel _model;
    private readonly ISoundProvider _soundProvider;

    private IEnumerator timer;

    public ChickenState_Nitro(IStateMachineProvider stateMachineProvider, ChickenUnitModel model, ISoundProvider soundProvider)
    {
        _machineProvider = stateMachineProvider;
        _model = model;
        _soundProvider = soundProvider;
    }

    public void EnterState()
    {
        Debug.Log("NITRO");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    private IEnumerator Timer()
    {
        _model.ActivateAnimation(ChickenAnimationType.Nitro);

        yield return new WaitForSeconds(0.1f);

        _soundProvider.PlayOneShot("Chicken_Nitro");

        _model.SetSpeed(Random.Range(450f, 500f), 0.1f);

        yield return new WaitForSeconds(3);

        ChangeStateToRun();
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private void ChangeStateToRun()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ChickenState_Run>());
    }
}
