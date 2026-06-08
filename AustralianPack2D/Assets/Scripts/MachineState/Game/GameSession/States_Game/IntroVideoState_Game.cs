using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroVideoState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IVideoProvider _videoProvider;
    private readonly IMaskEffectProvider _maskEffectProvider;

    private IEnumerator timer;

    public IntroVideoState_Game(IStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, IVideoProvider videoProvider, IMaskEffectProvider maskEffectProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _videoProvider = videoProvider;
        _maskEffectProvider = maskEffectProvider;
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
        _sceneRoot.OpenBackgroundBlackPanel();

        yield return new WaitForSeconds(0.3f);

        _videoProvider.Play("Intro");

        _sceneRoot.OpenIntroVideoPanel();

        _maskEffectProvider.Play("Intro", () =>
        {
            _sceneRoot.CloseBackgroundBlackPanel();
        });

        yield return new WaitForSeconds(2f);

        ChangeStateToPlayVideo();
    }

    private void ChangeStateToPlayVideo()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<PlayVideoState_Game>());
    }
}
