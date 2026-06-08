using System.Collections;
using UnityEngine;

public class ChickenState_UFO : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly ChickenUnitModel _model;
    private readonly ISoundProvider _soundProvider;

    private IEnumerator timer;

    public ChickenState_UFO(IStateMachineProvider stateMachineProvider, ChickenUnitModel model, ISoundProvider soundProvider)
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
        _model.EventGame_UFO_Activate();

        _soundProvider.PlayOneShot("Chicken_UFO");
        _model.SetSpeed(0, 0.2f);
        _model.ActivateAnimation(ChickenAnimationType.Abduction_Take);

        yield return new WaitForSeconds(2.8f);

        _model.ActivateAnimation(ChickenAnimationType.Abduction_Return);

        yield return new WaitForSeconds(0.2f);

        ChangeStateToRun();
    }

    public void ExitState()
    {
        _model.EventGame_UFO_Clear();

        if (timer != null) Coroutines.Stop(timer);
    }

    private void ChangeStateToRun()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ChickenState_Run>());
    }
}
