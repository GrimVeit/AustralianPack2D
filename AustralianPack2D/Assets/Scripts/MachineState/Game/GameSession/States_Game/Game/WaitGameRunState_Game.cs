using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitGameRunState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly ITimerProvider _timerProvider;
    private readonly ITimerListener _timerListener;
    private readonly ISoundProvider _soundProvider;

    private readonly ISound _sound_Main;

    public WaitGameRunState_Game(IStateMachineProvider machineProvider, ITimerProvider timerProvider_Start, ITimerListener timerListener_Start, ISoundProvider soundProvider)
    {
        _machineProvider = machineProvider;
        _timerProvider = timerProvider_Start;
        _timerListener = timerListener_Start;
        _soundProvider = soundProvider;

        _sound_Main = _soundProvider.GetSound("Background_Main");
    }

    public void EnterState()
    {
        _timerListener.OnTimeChanged += SetSound;
        _timerListener.OnStopTimer += ChangeStateToGameRun;

        _sound_Main.SetVolume(1f, 0.4f, 0.1f);
        _timerProvider.ActivateTimer(3, TimerDirection.Backward);
    }

    public void ExitState()
    {
        _timerListener.OnTimeChanged -= SetSound;
        _timerListener.OnStopTimer -= ChangeStateToGameRun;
    }

    private void SetSound(int value)
    {
        if(value == 0)
        {
            _soundProvider.PlayOneShot("Timer_Go");
        }
        else
        {
            _soundProvider.PlayOneShot("Timer_321");
        }
    }

    private void ChangeStateToGameRun()
    {
        _machineProvider.EnterState(_machineProvider.GetState<GameRunState_Game>());

        _timerProvider.DeactivateTimer();
    }
}
