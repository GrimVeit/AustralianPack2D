using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideoState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IVideoProvider _videoProvider;
    private readonly IMaskEffectProvider _maskEffectProvider;

    public PlayVideoState_Game(IStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, IVideoProvider videoProvider, IMaskEffectProvider maskEffectProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _videoProvider = videoProvider;
        _maskEffectProvider = maskEffectProvider;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToPlay_PLAY += ChangeStateToChooseLocation;
        _sceneRoot.OnClickToSettings_PLAY += ChangeStateToSettings;
        _sceneRoot.OnClickToCollection_PLAY += ChangeStateToCollection;

        _videoProvider.Play("Play");


        _maskEffectProvider.Play("Play", () =>
        {
            _maskEffectProvider.Stop("Intro");
            _sceneRoot.CloseIntroVideoPanel();
        });
        _sceneRoot.OpenPlayVideoPanel();
        _sceneRoot.OpenPlayPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToPlay_PLAY -= ChangeStateToChooseLocation;
        _sceneRoot.OnClickToSettings_PLAY -= ChangeStateToSettings;
        _sceneRoot.OnClickToCollection_PLAY -= ChangeStateToCollection;

        _sceneRoot.ClosePlayPanel();

        _sceneRoot.CloseIntroVideoPanel();
        _maskEffectProvider.Stop("Intro");
    }

    private void ChangeStateToChooseLocation()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<ChooseLocationState_Game>());
    }

    private void ChangeStateToSettings()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<SettingsState_Game>());

        _sceneRoot.ClosePlayVideoPanel();
    }

    private void ChangeStateToCollection()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<StartCardsState_Game>());

        _sceneRoot.ClosePlayVideoPanel();
    }
}
