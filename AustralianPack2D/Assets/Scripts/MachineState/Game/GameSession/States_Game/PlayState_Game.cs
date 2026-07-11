using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly ICardsOrchectrationProvider _cardsOrchectrationProvider;
    private readonly IGameScoreListener _gameScoreListener;

    private IEnumerator timer;

    public PlayState_Game(IStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, ICardsOrchectrationProvider cardsOrchectrationProvider, IGameScoreListener gameScoreListener)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _cardsOrchectrationProvider = cardsOrchectrationProvider;
        _gameScoreListener = gameScoreListener;
    }

    public void EnterState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _gameScoreListener.OnFinish += StartHoldOn;

        _cardsOrchectrationProvider.ActivateInteractive();
        _sceneRoot.OpenMainFooterPanel();
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _gameScoreListener.OnFinish -= StartHoldOn;

        _cardsOrchectrationProvider.DeactivateInteractive();
        _sceneRoot.CloseMainFooterPanel();
    }

    private void StartHoldOn()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = HoldOn();
        Coroutines.Start(timer);
    }

    private IEnumerator HoldOn()
    {
        yield return new WaitForSeconds(0.8f);

        ChangeStateToWinStart();
    }

    private void ChangeStateToWinStart()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<StartWinState_Game>());
    }
}
