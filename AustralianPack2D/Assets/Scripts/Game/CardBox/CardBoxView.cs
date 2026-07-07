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
    [SerializeField] private Transform transformCenter;
    [SerializeField] private Transform transformDown;
    [SerializeField] private float timeScale = 0.3f;

    private TrackEntry entry;

    private Tween tweenScale;

    public void Initialize()
    {
        entry = skeletonGraphic_Box.AnimationState.SetAnimation(0, "move", true);
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
        OnEndOpen?.Invoke();
    }

    private void ResetBox()
    {
        entry.TimeScale = 0f;
        entry.TrackTime = 0f;
    }

    public void Show()
    {
        ResetBox();

        tweenScale?.Kill();

        skeletonGraphic_Box.transform.localScale = Vector3.zero;

        tweenScale = skeletonGraphic_Box.transform.DOScale(1, timeScale);
    }

    public void ActivateOpen()
    {
        entry.TimeScale = 1f;
    }

    public void Hide()
    {
        tweenScale?.Kill();

        skeletonGraphic_Box.transform.localScale = Vector3.zero;

        skeletonGraphic_Box.gameObject.SetActive(false);
    }

    #region Output

    public event Action OnEndOpen;

    #endregion
}
