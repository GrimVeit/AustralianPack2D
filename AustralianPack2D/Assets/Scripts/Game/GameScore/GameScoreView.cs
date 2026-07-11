using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class GameScoreView : View
{
    [SerializeField] private TextMeshProUGUI textMoves;
    [SerializeField] private UIEffect effectPriority;
    [SerializeField] private UIEffect effectStandard;
    [SerializeField] private Color colorPriority;
    [SerializeField] private Color colorStandard;
    [SerializeField] private Color colorNone;

    [Header("COUNTER MOVE")]
    [SerializeField] private Vector3 vectorNone = Vector3.zero;
    [SerializeField] private Vector3 vectorGift = new(40, 0, 0);

    private Tween tweenMove;

    public void Initialize()
    {
        effectPriority.Initialize();
        effectStandard.Initialize();
    }

    public void Dispose()
    {
        effectPriority.Dispose();
        effectStandard.Dispose();
    }

    public void SetMove(int count, CardBoxType boxType)
    {
        textMoves.text = count.ToString();
        CheckEffect(boxType);
    }

    private void CheckEffect(CardBoxType boxType)
    {
        switch (boxType)
        {
            case CardBoxType.None:
                if(effectPriority.IsActive)
                   effectPriority.DeactivateEffect();

                if (effectStandard.IsActive)
                    effectStandard.DeactivateEffect();

                textMoves.DOColor(colorNone, 0.1f);

                MoveCounter(true);
                break;

            case CardBoxType.Standard:
                if (effectPriority.IsActive)
                    effectPriority.DeactivateEffect();

                if (!effectStandard.IsActive)
                    effectStandard.ActivateEffect();

                textMoves.DOColor(colorStandard, 0.1f);

                MoveCounter(false);
                break;
            case CardBoxType.Priority:
                if (!effectPriority.IsActive)
                    effectPriority.ActivateEffect();

                if (effectStandard.IsActive)
                    effectStandard.DeactivateEffect();

                textMoves.DOColor(colorPriority, 0.1f);

                MoveCounter(false);
                break;
        }
    }

    private void MoveCounter(bool isCenter)
    {
        tweenMove?.Kill();

        if (isCenter)
        {
            tweenMove = textMoves.transform.DOLocalMove(vectorNone, 0.2f);
        }
        else
        {
            tweenMove = textMoves.transform.DOLocalMove(vectorGift, 0.2f);
        }
    }
}
