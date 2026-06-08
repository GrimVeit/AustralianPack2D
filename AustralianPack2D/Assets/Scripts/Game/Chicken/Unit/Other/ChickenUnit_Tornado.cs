using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class ChickenUnit_Tornado : MonoBehaviour
{
    [SerializeField] private Transform transformUFO;
    [SerializeField] private SkeletonGraphic skeletonGraphic;

    private Sequence sequence;

    public void Activate()
    {
        sequence?.Kill();

        sequence = DOTween.Sequence();

        sequence.Append(transformUFO.DOScale(1, 0.2f));
        sequence.AppendInterval(1.5f);
        sequence.Append(transformUFO.DOScale(0, 0.2f));
    }

    public void Clear()
    {
        sequence?.Kill();

        sequence = DOTween.Sequence();
        sequence.Append(transformUFO.DOScale(0, 0.1f));
    }
}
