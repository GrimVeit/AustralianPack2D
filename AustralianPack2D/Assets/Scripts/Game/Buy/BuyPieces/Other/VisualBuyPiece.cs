using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VisualBuyPiece : MonoBehaviour
{
    public ChickenPicturePiece Data => _data;

    [SerializeField] private Image imagePiece;

    private ChickenPicturePiece _data;

    public void SetData(ChickenPicturePiece data)
    {
        _data = data;

        imagePiece.sprite = _data.Sprite;
    }

    public void Destroy()
    {
        transform.DOScale(0, 0.2f).OnComplete(() => OnDestroy?.Invoke(this));
    }

    public void SetSize(Vector2 size)
    {
        imagePiece.rectTransform.sizeDelta = size;
    }

    public void MoveTo(Vector2 pos, float duration)
    {
        transform.DOLocalMove(pos, duration);
    }

    #region Output

    public event Action<VisualBuyPiece> OnDestroy;

    #endregion
}
