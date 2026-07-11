using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly ICardsOrchectrationProvider _cardsOrchectrationProvider;

    public PlayState_Game(IStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, ICardsOrchectrationProvider cardsOrchectrationProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _cardsOrchectrationProvider = cardsOrchectrationProvider;
    }

    public void EnterState()
    {
        _cardsOrchectrationProvider.ActivateInteractive();
        _sceneRoot.OpenMainFooterPanel();
    }

    public void ExitState()
    {
        _cardsOrchectrationProvider.DeactivateInteractive();
        _sceneRoot.CloseMainFooterPanel();
    }
}
