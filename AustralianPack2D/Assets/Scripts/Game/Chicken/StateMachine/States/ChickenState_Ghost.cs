using System.Collections;
using UnityEngine;

public class ChickenState_Ghost : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly ChickenUnitModel _model;
    private readonly ISoundProvider _soundProvider;

    private IEnumerator timer;

    public ChickenState_Ghost(IStateMachineProvider stateMachineProvider, ChickenUnitModel model, ISoundProvider soundProvider)
    {
        _machineProvider = stateMachineProvider;
        _model = model;
        _soundProvider = soundProvider;
    }

    public void EnterState()
    {
        Debug.Log("GHOST");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    private IEnumerator Timer()
    {
        _model.EventGame_Ghost_Activate();

        yield return new WaitForSeconds(0.5f);

        _soundProvider.PlayOneShot("Chicken_Ghost");
        _model.SetSpeed(0, 0.2f);
        _model.ActivateAnimation(ChickenAnimationType.Fright);

        yield return new WaitForSeconds(1);

        ChangeStateToRun();
    }

    public void ExitState()
    {
        _model.EventGame_Ghost_Clear();

        if (timer != null) Coroutines.Stop(timer);
    }

    private void ChangeStateToRun()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ChickenState_Run>());
    }
}
