using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenState_Pigeon : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly ChickenUnitModel _model;
    private readonly ISoundProvider _soundProvider;

    private IEnumerator timer;

    public ChickenState_Pigeon(IStateMachineProvider stateMachineProvider, ChickenUnitModel model, ISoundProvider soundProvider)
    {
        _machineProvider = stateMachineProvider;
        _model = model;
        _soundProvider = soundProvider;
    }

    public void EnterState()
    {
        Debug.Log("PIGEON");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    private IEnumerator Timer()
    {
        _model.EventGame_Pigeon_ActivateStart();

        yield return new WaitForSeconds(0.5f);

        _soundProvider.PlayOneShot("Chicken_Pigeon");
        _model.SetSpeed(0, 0.2f);
        _model.ActivateAnimation(ChickenAnimationType.Pigeon);

        yield return new WaitForSeconds(1f);

        _model.EventGame_Pigeon_ActivateEnd();

        yield return new WaitForSeconds(0.1f);

        ChangeStateToRun();
    }

    public void ExitState()
    {
        _model.EventGame_Pigeon_Clear();

        if (timer != null) Coroutines.Stop(timer);
    }

    private void ChangeStateToRun()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ChickenState_Run>());
    }
}
