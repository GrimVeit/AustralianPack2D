using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly IStoreLevelInfo _storeLevelInfo;
    private readonly IStoreGameCardsProvider _gameCardsProvider;
    private readonly ICardsGameSpawnerProvider _cardsGameSpawnerProvider;

    public StartState_Game(IStateMachineProvider stateMachineProvider, IStoreLevelInfo storeLevelInfo, IStoreGameCardsProvider gameCardsProvider, ICardsGameSpawnerProvider cardsGameSpawnerProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _storeLevelInfo = storeLevelInfo;
        _gameCardsProvider = gameCardsProvider;
        _cardsGameSpawnerProvider = cardsGameSpawnerProvider;
    }

    public void EnterState()
    {
        _cardsGameSpawnerProvider.CreateGrid(_storeLevelInfo.GameLevel, _gameCardsProvider.CreateCards(LevelCardCountHelper.GetCardCount(_storeLevelInfo.GameLevel)));
    }

    public void ExitState()
    {

    }
}
