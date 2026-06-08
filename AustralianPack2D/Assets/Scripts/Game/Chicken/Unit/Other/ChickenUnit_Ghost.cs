using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChickenUnit_Ghost : MonoBehaviour
{
    [SerializeField] private Transform transformGhost;
    [SerializeField] private Transform transformStart;
    [SerializeField] private Transform transformEnd;

    private Sequence sequence;

    public void Activate()
    {
        sequence?.Kill();

        transformGhost.localPosition = transformStart.localPosition;
        transformGhost.localScale = Vector3.zero;

        sequence = DOTween.Sequence();

        sequence.Append(transformGhost.DOScale(1, 0.2f));
        sequence.Append(transformGhost.DOLocalMove(transformEnd.localPosition, 1.6f));
        sequence.Append(transformGhost.DOScale(0, 0.15f));
    }

    public void Clear()
    {
        sequence?.Kill();

        sequence = DOTween.Sequence();
        sequence.Append(transformGhost.DOScale(0, 0.1f));
    }
}
