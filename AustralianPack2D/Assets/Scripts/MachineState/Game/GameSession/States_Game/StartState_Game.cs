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

    public StartState_Game(IStateMachineProvider stateMachineProvider, IStoreLevelInfo storeLevelInfo, IStoreGameCardsProvider gameCardsProvider, ICardsGameSpawnerProvider cardsGameSpawnerProvider, IStoreCardDesignInfoProvider cardDesignInfoProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _storeLevelInfo = storeLevelInfo;
        _gameCardsProvider = gameCardsProvider;
        _cardsGameSpawnerProvider = cardsGameSpawnerProvider;
        _cardDesignInfoProvider = cardDesignInfoProvider;
    }

    public void EnterState()
    {
        _cardsGameSpawnerProvider.CreateGrid(_storeLevelInfo.GameLevel, _gameCardsProvider.CreateCards(LevelCardCountHelper.GetCardCount(_storeLevelInfo.GameLevel)), _cardDesignInfoProvider.CardDesignIndex);
    }

    public void ExitState()
    {

    }
}
