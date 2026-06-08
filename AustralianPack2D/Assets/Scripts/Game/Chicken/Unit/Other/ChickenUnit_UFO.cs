using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class ChickenUnit_UFO : MonoBehaviour
{
    [SerializeField] private Transform transformUFO;
    [SerializeField] private SkeletonGraphic skeletonGraphic;

    private Sequence sequence;

    public void Activate()
    {
        sequence?.Kill();

        sequence = DOTween.Sequence();

        sequence.Append(transformUFO.DOScale(1, 0.2f));
        sequence.AppendInterval(0.1f);
        sequence.AppendCallback(() => 
        {
            skeletonGraphic.AnimationState.ClearTracks();
            skeletonGraphic.AnimationState.SetAnimation(0, "abduction", false);
        });
        sequence.AppendInterval(3.1f);
        sequence.Append(transformUFO.DOScale(0, 0.15f));
    }

    public void Clear()
    {
        sequence?.Kill();

        sequence = DOTween.Sequence();
        sequence.Append(transformUFO.DOScale(0, 0.1f));
    }
}
