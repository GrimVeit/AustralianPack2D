using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BuyBoxView : View
{
    [SerializeField] private Transform transformBoxMain;
    [SerializeField] private Transform transformBoxTop;

    [Header("POINTS")]
    [SerializeField] private List<Transform> transformsStartPos = new();
    [SerializeField] private Transform transformCenter;
    [SerializeField] private Transform transformDownCenter;

    private Tween tweenScale;
    private Tween tweenMove;

    public void SetStartPosition(int id)
    {
        tweenScale?.Kill();
        tweenMove?.Kill();

        transformBoxMain.localPosition = transformsStartPos[id].localPosition;
    }

    public void MoveToCenter(float duration, Action OnComplete = null)
    {
        MoveTo(transformCenter.localPosition, duration, OnComplete);
    }

    public void MoveToCenterDown(float duration, Action OnComplete = null)
    {
        MoveTo(transformDownCenter.localPosition, duration, OnComplete);
    }

    public void OpenClose(bool value)
    {
        transformBoxMain.gameObject.SetActive(value);
    }

    public void SetScale(float value)
    {
        tweenScale?.Kill();

        transformBoxMain.localScale = new Vector3(value, value, value);
    }

    public void ScaleTo(float value, float duration, Action OnComplete = null)
    {
        tweenScale?.Kill();

        tweenScale = transformBoxMain.DOScale(new Vector3(value, value, value), duration).OnComplete(() => OnComplete?.Invoke());
    }

    public void MoveTo(Vector3 vector, float duration, Action OnComplete = null)
    {
        tweenMove?.Kill();

        tweenMove = transformBoxMain.DOLocalMove(vector, duration).OnComplete(() => OnComplete?.Invoke());
    }


    public void OpenCover(float duration, Action OnComplete = null)
    {
        transformBoxTop.DOLocalMove(new Vector3(0, -230, 0), duration).OnComplete(() => OnComplete?.Invoke());
    }

    public void CloseCover(float duration, Action OnComplete = null)
    {
        transformBoxTop.DOLocalMove(new Vector3(0, 5, 0), duration).OnComplete(() => OnComplete?.Invoke());
    }
}
