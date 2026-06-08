using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class ChickenUnit_Pigeon : MonoBehaviour
{
    [SerializeField] private Transform transformPigeon;
    [SerializeField] private Transform transformStart;
    [SerializeField] private Transform transformChicken;
    [SerializeField] private Transform transformEnd;
    [SerializeField] private SkeletonGraphic skeletonGraphic;

    private Sequence sequence;

    public void ActivateStart()
    {
        sequence?.Kill();

        skeletonGraphic.AnimationState.SetAnimation(0, "Fly", true);
        transformPigeon.localPosition = transformStart.localPosition;
        transformPigeon.localScale = Vector3.zero;

        sequence = DOTween.Sequence();

        sequence.Append(transformPigeon.DOScale(1, 0.2f));
        sequence.AppendInterval(0.1f);
        sequence.Append(transformPigeon.DOLocalMove(transformChicken.localPosition, 0.5f));
        sequence.AppendCallback(() =>
        {
            skeletonGraphic.AnimationState.SetAnimation(0, "attack", true);
        });
    }

    public void ActivateEnd()
    {
        sequence?.Kill();

        skeletonGraphic.AnimationState.SetAnimation(0, "Fly", true);

        sequence = DOTween.Sequence();

        sequence.Append(transformPigeon.DOLocalMove(transformEnd.localPosition, 0.5f));
        sequence.Append(transformPigeon.DOScale(0, 0.15f));
    }

    public void Clear()
    {
        sequence?.Kill();

        sequence = DOTween.Sequence();
        sequence.Append(transformPigeon.DOScale(0, 0.15f));
    }
}
