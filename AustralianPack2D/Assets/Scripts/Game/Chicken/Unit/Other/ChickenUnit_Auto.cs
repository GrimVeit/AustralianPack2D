using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChickenUnit_Auto : MonoBehaviour
{
    [SerializeField] private Transform transformAuto;
    [SerializeField] private Transform transformStart;
    [SerializeField] private Transform transformEnd;

    private Sequence sequence;

    public void Activate()
    {
        sequence?.Kill();

        transformAuto.localPosition = transformStart.localPosition;
        transformAuto.localScale = Vector3.zero;

        sequence = DOTween.Sequence();

        sequence.Append(transformAuto.DOScale(1, 0.2f));
        sequence.Append(transformAuto.DOLocalMove(transformEnd.localPosition, 1));
        sequence.Append(transformAuto.DOScale(0, 0.15f));
    }

    public void Clear()
    {
        sequence?.Kill();

        sequence = DOTween.Sequence();
        sequence.Append(transformAuto.DOScale(0, 0.1f));
    }
}
