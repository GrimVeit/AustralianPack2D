using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameRunState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IMaskEffectProvider _maskEffectProvider;

    private IEnumerator timer;

    public StartGameRunState_Game(IStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, IMaskEffectProvider maskEffectProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _maskEffectProvider = maskEffectProvider;
    }

    public void EnterState()
    {
        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        _maskEffectProvider.Stop("StartGame");

        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        _sceneRoot.OpenMainPanel();
        _sceneRoot.CloseBackgroundBrownPanel();
        _sceneRoot.CloseChooseLocationPanel();

        yield return new WaitForSeconds(0.2f);

        ChangeStateToChickenSpawn();
    }

    private void ChangeStateToChickenSpawn()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<ChickenSpawnState_Game>());
    }
}
