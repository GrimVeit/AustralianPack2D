using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIGameRoot _sceneRoot;

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _sound_Main;

    public LoseState_Game(IStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, ISoundProvider soundProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _soundProvider = soundProvider;
        _sound_Main = _soundProvider.GetSound("Background_Main");
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToRestart_LOSE += ChangeStateToRestart;
        _sceneRoot.OnClickToMenu_LOSE += ChangeStateToMenu;
        _sceneRoot.OnClickToExit_LOSE += ChangeStateToExit;

        _sceneRoot.OpenBackgroundBrownPanel();
        _sceneRoot.OpenLosePanel();

        _sound_Main.Play();
        _sound_Main.SetVolume(0, 1f, 0.1f);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToRestart_LOSE -= ChangeStateToRestart;
        _sceneRoot.OnClickToMenu_LOSE -= ChangeStateToMenu;
        _sceneRoot.OnClickToExit_LOSE -= ChangeStateToExit;

        _sceneRoot.CloseLosePanel();
        _sceneRoot.CloseLoseVideoPanel();
    }

    private void ChangeStateToRestart()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<ChooseLocationState_Game>());
    }

    private void ChangeStateToMenu()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<PlayVideoState_Game>());

        _sceneRoot.CLoseMainPanel();
        _sceneRoot.CloseBackgroundBrownPanel();
    }

    private void ChangeStateToExit()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<StartGameRunState_Game>());

        _sceneRoot.CloseBackgroundBrownPanel();
    }
}
