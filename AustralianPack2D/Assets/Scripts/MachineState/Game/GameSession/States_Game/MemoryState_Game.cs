using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IStoreLevelInfo _storeLevelInfo;
    private readonly ICardsOrchectrationProvider _cardsOrchectrationProvider;

    private IEnumerator timer;

    private readonly Dictionary<GameLevel, float> _timesMemorize = new Dictionary<GameLevel, float>()
    {
        { GameLevel.Level1_4, 0.5f },
        { GameLevel.Level2_8, 1f},
        { GameLevel.Level3_16, 2f },
        { GameLevel.Level4_32, 3f },
        { GameLevel.Level5_64, 5f }
    };

    public MemoryState_Game(IStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, IStoreLevelInfo storeLevelInfo, ICardsOrchectrationProvider cardsOrchectrationProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _storeLevelInfo = storeLevelInfo;
        _cardsOrchectrationProvider = cardsOrchectrationProvider;
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        _cardsOrchectrationProvider.ShowCards();
        _sceneRoot.OpenMemorizeFooterPanel();

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _cardsOrchectrationProvider.HideCards();
        _sceneRoot.CloseMemorizeFooterPanel();
    }

    private IEnumerator Timer()
    {
        if(_timesMemorize.TryGetValue(_storeLevelInfo.GameLevel, out float time))
        {
            yield return new WaitForSeconds(time);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        ChangeStateToPlay();
    }

    private void ChangeStateToPlay()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<PlayState_Game>());
    }
}
