using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDesignBuyVisual : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private int price;

    [Header("Choose/Unchoose")]
    [SerializeField] private Image imageFrame;

    [Header("Select/Deselect")]
    [SerializeField] private Transform transformCheck;

    [Header("Open/Close")]
    [SerializeField] private Transform transformShadow;
    [SerializeField] private Transform transformPrice;
    [SerializeField] private Button buttonBuy;

    public void Initialize()
    {
        buttonBuy.onClick.AddListener(Buy);
    }

    public void Dispose()
    {
        buttonBuy.onClick.RemoveListener(Buy);
    }

    public void Open(float timeOpenCloseShadow, float timeOpenClosePrice)
    {
        transformShadow.DOScaleY(0, timeOpenCloseShadow);
        transformPrice.DOScale(0, timeOpenClosePrice);
        buttonBuy.enabled = false;
        buttonBuy.transform.DOScale(0, timeOpenClosePrice);
    }

    public void Close(float timeOpenCloseShadow, float timeOpenClosePrice)
    {
        transformShadow.DOScaleY(1, timeOpenCloseShadow);
        transformPrice.DOScale(1, timeOpenClosePrice);
        buttonBuy.enabled = true;
        buttonBuy.transform.DOScale(1, timeOpenClosePrice);
    }

    public void Select(float timeSelectDeselectCheck)
    {
        transformCheck.DOScale(1, timeSelectDeselectCheck);
    }

    public void Deselect(float timeSelectDeselectCheck)
    {
        transformCheck.DOScale(0, timeSelectDeselectCheck);
    }

    public void Choose(float timeChooseUnchooseCheck)
    {
        imageFrame.DOFade(1, timeChooseUnchooseCheck);
    }

    public void Unchoose(float timeChooseUnchooseCheck)
    {
        imageFrame.DOFade(0, timeChooseUnchooseCheck);
    }



    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnChoose?.Invoke(id);
    }

    #region Output

    public event Action<int> OnChoose;
    public event Action<int, int> OnBuy;

    private void Buy()
    {
        OnBuy?.Invoke(id, price);
    }

    #endregion
}
