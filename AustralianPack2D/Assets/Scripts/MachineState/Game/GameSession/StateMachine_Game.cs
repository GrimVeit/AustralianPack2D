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
        ICardsOrchectrationProvider cardsOrchectrationProvider,
        IVideoProvider videoProvider,
        ISoundProvider soundProvider,
        IGameScoreListener gameScoreListener
        )
    {
        states[typeof(StartHoldOnState_Game)] = new StartHoldOnState_Game(this);
        states[typeof(StartState_Game)] = new StartState_Game(this, storeLevelInfo, storeGameCardsProvider, cardsGameSpawnerProvider, storeCardDesignInfoProvider, cardsGameSpawnerListener, sceneRoot);
        states[typeof(MemoryState_Game)] = new MemoryState_Game(this, sceneRoot, storeLevelInfo, cardsOrchectrationProvider);
        states[typeof(PlayState_Game)] = new PlayState_Game(this, sceneRoot, cardsOrchectrationProvider, gameScoreListener);

        states[typeof(StartWinState_Game)] = new StartWinState_Game(this, videoProvider, sceneRoot, soundProvider);
        states[typeof(WinState_Game)] = new WinState_Game(this, sceneRoot);
    }

    public void Initialize()
    {
        EnterState(GetState<StartHoldOnState_Game>());
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
