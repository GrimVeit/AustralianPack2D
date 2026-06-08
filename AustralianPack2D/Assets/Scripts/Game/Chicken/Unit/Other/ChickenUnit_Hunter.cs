using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class ChickenUnit_Hunter : MonoBehaviour
{
    [SerializeField] private Transform transformAuto;
    [SerializeField] private SkeletonGraphic skeletonGraphic;

    private Sequence sequence;

    public void Activate()
    {
        sequence?.Kill();

        transformAuto.localScale = Vector3.zero;

        sequence = DOTween.Sequence();

        sequence.Append(transformAuto.DOScale(1, 0.15f));
        sequence.AppendCallback(() =>
        {
            skeletonGraphic.AnimationState.SetAnimation(0, "hunter", false);
        });
    }

    public void Clear()
    {
        sequence?.Kill();

        sequence = DOTween.Sequence();
        sequence.Append(transformAuto.DOScale(0, 0.15f));
    }
}
