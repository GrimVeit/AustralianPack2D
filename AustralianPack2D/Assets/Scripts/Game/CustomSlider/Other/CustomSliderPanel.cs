using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomSliderPanel : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        OnUpdatePercent?.Invoke(eventData, false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnUpdatePercent?.Invoke(eventData, true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUp?.Invoke();
    }

    public event Action<PointerEventData, bool> OnUpdatePercent;
    public event Action OnUp;
}

[System.Serializable]
public class SliderBlock
{
    public Image imageBlock;
    public Color colorActive;
    public Color colorInactive;

    [Range(0, 100)]
    public float percentValue;
}
