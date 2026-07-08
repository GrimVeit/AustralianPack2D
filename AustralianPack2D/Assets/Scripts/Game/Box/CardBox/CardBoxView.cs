using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine;
using Spine.Unity;
using UnityEngine;

public class CardBoxView : View
{
    [SerializeField] private SkeletonGraphic skeletonGraphic_Box;
    [SerializeField] private Transform transformBox;
    [SerializeField] private Transform transformCenter;
    [SerializeField] private Transform transformDown;

    private TrackEntry entry;

    private Tween tweenScale;
    private Sequence sequenceMove;
    private Tween tweenMove;

    private bool isReadyCheckComplete = false;

    public void Initialize()
    {
        entry = skeletonGraphic_Box.AnimationState.SetAnimation(0, "opening", false);
        entry.Complete += OnAnimationComplete;

        Hide();
        ResetBox();
    }

    public void Dispose()
    {
        entry.Complete -= OnAnimationComplete;
    }

    private void OnAnimationComplete(TrackEntry entry)
    {
        if(!isReadyCheckComplete) return;

        OnEndOpen?.Invoke();

        isReadyCheckComplete = false;
    }

    private void ResetBox()
    {
        transformBox.transform.localPosition = transformCenter.localPosition;

        entry.TimeScale = 0f;
        entry.TrackTime = 0f;
    }

    public void SetSkin(CardBoxType type)
    {
        switch (type)
        {
            case CardBoxType.Standard:
                skeletonGraphic_Box.Skeleton.SetSkin("box_standard");
                skeletonGraphic_Box.Skeleton.SetSlotsToSetupPose();
                break;
            case CardBoxType.Priority:
                skeletonGraphic_Box.Skeleton.SetSkin("priority");
                skeletonGraphic_Box.Skeleton.SetSlotsToSetupPose();
                break;
            default:
                skeletonGraphic_Box.Skeleton.SetSkin("box_standard");
                skeletonGraphic_Box.Skeleton.SetSlotsToSetupPose();
                break;
        }
    }

    public void Show(float time)
    {
        sequenceMove?.Kill();
        tweenScale?.Kill();
        tweenMove?.Kill();

        ResetBox();

        transformBox.localScale = Vector3.zero;

        transformBox.gameObject.SetActive(true);

        tweenScale = transformBox.DOScale(1, time);
    }

    public void ActivateOpen()
    {
        sequenceMove?.Kill();
        tweenMove?.Kill();

        isReadyCheckComplete = true;
        entry.TimeScale = 0.8f;

        sequenceMove = DOTween.Sequence();

        sequenceMove.AppendInterval(2f).AppendCallback(() =>
        {
            MoveDown();
        });

    }

    private void MoveDown()
    {
        tweenMove = transformBox.DOLocalMove(transformDown.localPosition, 1f);
    }

    public void Hide()
    {
        sequenceMove?.Kill();
        tweenScale?.Kill();
        tweenMove?.Kill();

        ResetBox();

        transformBox.localScale = Vector3.zero;
        transformBox.gameObject.SetActive(false);
    }

    #region Output

    public event Action OnEndOpen;

    #endregion
}

public enum CardBoxType
{
    None, Standard, Priority
}
