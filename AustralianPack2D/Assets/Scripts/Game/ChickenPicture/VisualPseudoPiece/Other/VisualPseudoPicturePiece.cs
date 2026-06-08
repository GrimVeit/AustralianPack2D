using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisualPseudoPicturePiece : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ChickenPicturePiece Data => _data;

    public event Action<VisualPseudoPicturePiece> OnGrabbing;
    public event Action OnStartMove;
    public event Action<VisualPseudoPicturePiece, RectTransform> OnEndMove;
    public event Action<Vector2> OnMove;

    [SerializeField] private Image imagePiece;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private ChickenPicturePiece _data;

    private Vector2 _mainPos;

    public void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Dispose()
    {

    }

    public void SetMainPos(Vector2 pos)
    {
        _mainPos = pos;
    }

    public void SetData(ChickenPicturePiece data)
    {
        _data = data;

        imagePiece.sprite = data.Sprite;
    }

    public void SetSize(Vector2 size)
    {
        rectTransform.sizeDelta = size;
    }

    #region Methods

    public void Teleport()
    {
        canvasGroup.blocksRaycasts = true;

        rectTransform.localPosition = _mainPos;
    }

    public void StartMove()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void EndMove()
    {
        canvasGroup.blocksRaycasts = true;

        rectTransform.DOLocalMove(_mainPos, 0.1f);
    }


    public void Move(Vector2 vector)
    {
        rectTransform.anchoredPosition += vector;
    }

    #endregion

    #region Input

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnGrabbing?.Invoke(this);
        OnStartMove?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnMove?.Invoke(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndMove?.Invoke(this, rectTransform);
    }

    #endregion
}
