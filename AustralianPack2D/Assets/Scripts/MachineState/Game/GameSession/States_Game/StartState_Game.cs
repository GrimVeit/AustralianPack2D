using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly IStoreLevelInfo _storeLevelInfo;
    private readonly IStoreGameCardsProvider _gameCardsProvider;
    private readonly ICardsGameSpawnerProvider _cardsGameSpawnerProvider;
    private readonly IStoreCardDesignInfoProvider _cardDesignInfoProvider;
    private readonly ICardsGameSpawnerListener _cardsGameSpawnerListener;
    private readonly UIGameRoot _sceneRoot;

    public StartState_Game(IStateMachineProvider stateMachineProvider, IStoreLevelInfo storeLevelInfo, IStoreGameCardsProvider gameCardsProvider, ICardsGameSpawnerProvider cardsGameSpawnerProvider, IStoreCardDesignInfoProvider cardDesignInfoProvider, ICardsGameSpawnerListener cardsGameSpawnerListener, UIGameRoot sceneRoot)
    {
        _stateMachineProvider = stateMachineProvider;
        _storeLevelInfo = storeLevelInfo;
        _gameCardsProvider = gameCardsProvider;
        _cardsGameSpawnerProvider = cardsGameSpawnerProvider;
        _cardDesignInfoProvider = cardDesignInfoProvider;
        _cardsGameSpawnerListener = cardsGameSpawnerListener;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _cardsGameSpawnerListener.OnCreateGrid += ChangeStateToMemorize;

        _sceneRoot.OpenMainPanel();
        _cardsGameSpawnerProvider.CreateGrid(_storeLevelInfo.GameLevel, _gameCardsProvider.CreateCards(LevelCardCountHelper.GetCardCount(_storeLevelInfo.GameLevel)), _cardDesignInfoProvider.CardDesignIndex);
    }

    public void ExitState()
    {
        _cardsGameSpawnerListener.OnCreateGrid -= ChangeStateToMemorize;
    }

    private void ChangeStateToMemorize()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<MemoryState_Game>());
    }
}
