using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly IChickenBattleProvider _chickenBattleProvider;
    private readonly IChickenBattleListener _chickenBattleListener;
    private readonly UIGameRoot _sceneRoot;

    private readonly ITimerProvider _timerProvider;
    private readonly IChickenRaceLeaderProvider _chickenRaceLeaderProvider;
    private readonly ICameraFollowProvider _cameraFollowProvider;
    private readonly IChooseChickenProvider _chooseChickenProvider;

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _sound_Main;
    private readonly ISound _sound_Run;

    private IEnumerator timer;

    public GameRunState_Game(IStateMachineProvider stateMachineProvider, IChickenBattleProvider chickenBattleProvider, IChickenBattleListener chickenBattleListener, UIGameRoot sceneRoot, ITimerProvider timerProvider, IChickenRaceLeaderProvider chickenRaceLeaderProvider, ICameraFollowProvider cameraFollowProvider, IChooseChickenProvider chooseChickenProvider, ISoundProvider soundProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _chickenBattleProvider = chickenBattleProvider;
        _chickenBattleListener = chickenBattleListener;
        _sceneRoot = sceneRoot;
        _timerProvider = timerProvider;
        _chickenRaceLeaderProvider = chickenRaceLeaderProvider;
        _cameraFollowProvider = cameraFollowProvider;
        _chooseChickenProvider = chooseChickenProvider;
        _soundProvider = soundProvider;

        _sound_Main = _soundProvider.GetSound("Background_Main");
        _sound_Run = _soundProvider.GetSound("Background_Run");
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);

        _chickenBattleListener.OnEndGame += ChangeStateToCheckWinnerState;
        _sceneRoot.OnClickToMenu_MAINHEADER += ChangeStateToMenu;

        _chickenBattleProvider.StartGame();
        _sceneRoot.OpenMainHeaderPanel();
        _timerProvider.ActivateTimer(3600, TimerDirection.Forward);

        _sound_Main.SetVolume(0.4f, 0, 0.1f, _sound_Main.Stop);
        _sound_Run.Play();
        _sound_Run.SetVolume(0, 1, 0.1f);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _chickenBattleListener.OnEndGame -= ChangeStateToCheckWinnerState;
        _sceneRoot.OnClickToMenu_MAINHEADER -= ChangeStateToMenu;

        _sceneRoot.CloseMainHeaderPanel();
        _timerProvider.DeactivateTimer();
        _chickenRaceLeaderProvider.Deactivate();

        _sound_Run.SetVolume(1, 0, 0.1f, _sound_Run.Stop);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);

        _chickenRaceLeaderProvider.Activate();
    }

    private void ChangeStateToCheckWinnerState()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<CheckWinnerState_Game>());
    }

    private void ChangeStateToMenu()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<PlayVideoState_Game>());

        _cameraFollowProvider.Clear();
        _chickenBattleProvider.StopBattle();
        _chooseChickenProvider.HideAll();
        _sceneRoot.CLoseMainPanel();
    }
}
