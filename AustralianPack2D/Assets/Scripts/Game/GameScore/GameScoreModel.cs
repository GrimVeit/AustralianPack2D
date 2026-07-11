using System;
using UnityEngine;

public class GameScoreModel
{
    public CardBoxType CardBoxType => _cardBoxType;

    public bool IsGoodResult;

    private readonly ICardsOrchectrationListener _flow;
    private readonly IStoreLevelInfo _levelInfo;

    private int _moves = 0;
    private int _matches;

    private readonly LevelScoreConfig _config;

    private CardBoxType _cardBoxType;

    public GameScoreModel(ICardsOrchectrationListener flow, IStoreLevelInfo levelInfo)
    {
        _flow = flow;
        _levelInfo = levelInfo;

        _config = HelperLevelScoreBalance.GetConfig(_levelInfo.GameLevel);
        Debug.Log($"Level - {_levelInfo.GameLevel}");

        _flow.OnAddMove += OnMove;
        _flow.OnAddMatch += OnMatch;

        _cardBoxType = _config.StartGift;
    }

    public void Initialize()
    {
        OnChangeMoves?.Invoke(_moves, _cardBoxType);
    }

    public void Dispose()
    {
        _flow.OnAddMove -= OnMove;
        _flow.OnAddMatch -= OnMatch;
    }

    private void OnMove()
    {
        _moves += 1;

        switch (_config.StartGift)
        {
            case CardBoxType.None:
                _cardBoxType = CardBoxType.None;
                break;
            case CardBoxType.Standard:

                if (_moves >= 0 && _moves <= _config.StandardMoveThreshold)
                {
                    _cardBoxType = CardBoxType.Standard;
                }
                else
                {
                    _cardBoxType = CardBoxType.None;
                }

                break;
            case CardBoxType.Priority:

                if (_moves >= 0 && _moves <= _config.PriorityMoveThreshold)
                {
                    _cardBoxType = CardBoxType.Priority;
                }
                else if (_moves > _config.PriorityMoveThreshold && _moves <= _config.StandardMoveThreshold)
                {
                    _cardBoxType = CardBoxType.Standard;
                }
                else
                {
                    _cardBoxType = CardBoxType.None;
                }

                break;
            default:
                break;
        }

        OnChangeMoves.Invoke(_moves, _cardBoxType);
    }

    private void OnMatch()
    {
        _matches += 1;

        Debug.Log($"MATCHES - {_matches}, TOTAL - {_config.TotalPairs}");

        if (_matches >= _config.TotalPairs)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        Debug.Log("FINISH GAME");
        OnFinish?.Invoke();
    }

    #region Output

    public event Action<int, CardBoxType> OnChangeMoves;
    public event Action OnFinish;

    #endregion
}
