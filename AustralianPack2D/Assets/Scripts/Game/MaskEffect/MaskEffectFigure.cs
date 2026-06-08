using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MaskEffectFigure : MonoBehaviour
{
    [SerializeField] private Image figureImage;
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite SquareSprite;

    private Tween tweenScale;

    public void SetType(MaskFigureType type)
    {
        switch (type)
        {
            case MaskFigureType.Circle:
                figureImage.sprite = circleSprite;
                break;
            case MaskFigureType.Square:
                figureImage.sprite = SquareSprite;
                break;
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(false);
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void SetPosition(Vector3 vector)
    {
        transform.localPosition = vector;
    }

    public void SetPosition(Vector3 vector, float duration)
    {
        transform.DOLocalMove(vector, duration);
    }

    public void Show(float duration, float scale, Action OnComplete = null)
    {
        tweenScale?.Kill();

        tweenScale = figureImage.transform.DOScale(scale, duration).OnComplete(() => { OnComplete?.Invoke(); });
    }

    public void Hide(float duration, Action OnComplete = null)
    {
        tweenScale?.Kill();

        tweenScale = figureImage.transform.DOScale(0, duration).OnComplete(() => { OnComplete?.Invoke(); });
    }

    public void SetSize(float size, float duration, Action OnComplete = null)
    {
        tweenScale?.Kill();

        tweenScale = figureImage.transform.DOScale(size, duration).OnComplete(() => { OnComplete?.Invoke(); });
    }
}

public enum MaskFigureType
{
    Circle, Square
}
