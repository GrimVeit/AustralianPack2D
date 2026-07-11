using System;
using System.Collections.Generic;

public class StateMachine_Game : IStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Game
        (IStoreLevelInfo storeLevelInfo,
        IStoreGameCardsProvider storeGameCardsProvider,
        ICardsGameSpawnerProvider cardsGameSpawnerProvider,
        IStoreCardDesignInfoProvider storeCardDesignInfoProvider,
        ICardsGameSpawnerListener cardsGameSpawnerListener,
        UIGameRoot sceneRoot,
        ICardsOrchectrationProvider cardsOrchectrationProvider
        )
    {
        states[typeof(StartState_Game)] = new StartState_Game(this, storeLevelInfo, storeGameCardsProvider, cardsGameSpawnerProvider, storeCardDesignInfoProvider, cardsGameSpawnerListener, sceneRoot);
        states[typeof(MemoryState_Game)] = new MemoryState_Game(this, sceneRoot, storeLevelInfo, cardsOrchectrationProvider);
        states[typeof(PlayState_Game)] = new PlayState_Game(this, sceneRoot, cardsOrchectrationProvider);
    }

    public void Initialize()
    {
        EnterState(GetState<StartState_Game>());
    }

    public void Dispose()
    {
        _currentState?.ExitState();
    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void EnterState(IState state)
    {
        _currentState?.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
