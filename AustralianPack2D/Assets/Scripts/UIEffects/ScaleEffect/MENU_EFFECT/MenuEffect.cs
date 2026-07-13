using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuEffect : UIEffect
{
    [Header("MAIN")]
    [SerializeField] private Transform transformMain;
    [SerializeField] private float timeMainScale;
    [Header("Avatar")]
    [SerializeField] private Transform transformAvatar;
    [SerializeField] private Vector3 vectorAvatarCenter;
    [SerializeField] private Vector3 vectorAvatarLeft;
    [SerializeField] private float timeAvatarMove;
    [SerializeField] private float timeAvatarScale;
    [Header("Text")]
    [SerializeField] private TypeTextEffect typeTextEffect;

    private Sequence seq;

    public override void Initialize()
    {
        seq?.Kill();

        ResetEffect();
    }

    public override void Dispose()
    {

    }

    public override void ActivateEffect(Action OnComplete = null)
    {
        seq?.Kill();

        isActive = true;

        transformMain.localScale = Vector3.zero;
        transformAvatar.localScale = Vector3.zero;
        transformAvatar.localPosition = vectorAvatarCenter;
        typeTextEffect.ResetEffect();

        seq = DOTween.Sequence();

        seq.Append(transformMain.DOScale(1, timeMainScale).SetEase(Ease.OutBack)).
            Append(transformAvatar.DOScale(1, timeAvatarMove).SetEase(Ease.OutBack)). 
            Append(transformAvatar.DOLocalMove(vectorAvatarLeft, timeAvatarScale)).OnComplete(() =>
            {
                typeTextEffect.ActivateEffect();
            });
    }

    public override void DeactivateEffect(Action OnComplete = null)
    {
        seq?.Kill();

        isActive = false;

        seq = DOTween.Sequence();

        seq.Append(transformMain.DOScale(0, timeMainScale)).
            Join(transformAvatar.DOScale(0, timeAvatarMove)).
            Join(transformAvatar.DOLocalMove(vectorAvatarCenter, timeAvatarScale)).OnComplete(() =>
            {
                typeTextEffect.DeactivateEffect();
            });
    }

    public override void ResetEffect()
    {
        isActive = false;

        transformMain.localScale = Vector3.zero;
        transformAvatar.localScale = Vector3.zero;
        transformAvatar.localPosition = vectorAvatarCenter;
        typeTextEffect.ResetEffect();
    }
}