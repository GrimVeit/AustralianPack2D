using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class ChickenUnit_Animations : MonoBehaviour
{
    [SerializeField] private ChickenAnimation animations;

    public void Show()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(Vector3.one, 0.25f)
            .SetEase(Ease.OutBack);
    }

    public void HideDestroy()
    {
        transform.DOScale(Vector3.zero, 0.25f)
        .SetEase(Ease.InBack)
        .OnComplete(() => Destroy(gameObject));
    }

    public void SetOrder(int order)
    {
        animations.SetOrder(order);
    }

    public void SetSkin(string name)
    {
        animations.SetSkin(name);
    }

    public void ActivateAnimation(ChickenAnimationType animationType)
    {
        animations.ActivateAnimation(animationType);
    }

    #region Output

    public event Action OnClick;

    private void ClickItem()
    {
        OnClick?.Invoke();
    }

    #endregion
}

[Serializable]
public class ChickenAnimation
{
    [SerializeField] private SkeletonGraphic skeletonGraphic;
    [SerializeField] private List<ChickAnimationType> skeletonAnimationTypes = new List<ChickAnimationType>();

    public void SetOrder(int order)
    {
        skeletonGraphic.GetComponent<MeshRenderer>().sortingOrder = order;
    }

    public void SetSkin(string name)
    {
        skeletonGraphic.Skeleton.SetSkin(name);
        skeletonGraphic.Skeleton.SetSlotsToSetupPose(); // сбрасывает слоты на новый скин
        skeletonGraphic.Update(0);
    }

    public void Activate()
    {
        skeletonGraphic.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        skeletonGraphic.gameObject.SetActive(false);
    }

    public void ActivateAnimation(ChickenAnimationType animationEnum)
    {
        var skeletonAnimationType = GetSkeletonAnimationType(animationEnum);
        if (skeletonAnimationType == null) return;

        skeletonGraphic.AnimationState.SetAnimation(0, skeletonAnimationType.Key, skeletonAnimationType.IsLoop);
    }

    private ChickAnimationType GetSkeletonAnimationType(ChickenAnimationType animationEnum)
    {
        return skeletonAnimationTypes.Find(data => data.AnimationEnum == animationEnum);
    }
}

[Serializable]
public class ChickAnimationType
{
    [SerializeField] private string key;
    [SerializeField] private ChickenAnimationType animationEnum;
    [SerializeField] private bool isLoop;

    public string Key => key;
    public ChickenAnimationType AnimationEnum => animationEnum;
    public bool IsLoop => isLoop;
}
