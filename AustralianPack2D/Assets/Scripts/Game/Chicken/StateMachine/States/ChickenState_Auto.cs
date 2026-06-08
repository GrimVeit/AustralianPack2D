using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenState_Auto : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly ChickenUnitModel _model;
    private readonly ISoundProvider _soundProvider;

    private IEnumerator timer;

    public ChickenState_Auto(IStateMachineProvider stateMachineProvider, ChickenUnitModel model, ISoundProvider soundProvider)
    {
        _machineProvider = stateMachineProvider;
        _model = model;
        _soundProvider = soundProvider;
    }

    public void EnterState()
    {
        Debug.Log("AUTO");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    private IEnumerator Timer()
    {
        _model.EventGame_Auto_Activate();

        _soundProvider.PlayOneShot("Chicken_Car");
        yield return new WaitForSeconds(0.4f);

        _model.SetSpeed(0, 0.2f);
        _model.ActivateAnimation(ChickenAnimationType.HitCar);

        yield return new WaitForSeconds(3);

        ChangeStateToRun();
    }

    public void ExitState()
    {
        _model.EventGame_Auto_Clear();

        if (timer != null) Coroutines.Stop(timer);
    }

    private void ChangeStateToRun()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ChickenState_Run>());
    }
}
