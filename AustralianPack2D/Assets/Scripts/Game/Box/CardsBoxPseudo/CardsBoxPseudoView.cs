using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardsBoxPseudoView : View
{
    [SerializeField] private List<CardPseudoItem> cardsItem = new();
    [SerializeField] private List<Transform> transformsPoses = new();

    [SerializeField] private Vector2 sizeStart;
    [SerializeField] private Vector2 sizeEnd;

    [SerializeField] private Transform transformStart;

    [SerializeField] private Sprite spriteStandard;
    [SerializeField] private Sprite spritePriority;

    public void Initialize()
    {
        for (int i = 0; i < cardsItem.Count; i++)
        {
            cardsItem[i].OnEndMove += EndMove;
            cardsItem[i].OnEndRotate += EndRotate;
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < cardsItem.Count; i++)
        {
            cardsItem[i].OnEndMove -= EndMove;
            cardsItem[i].OnEndRotate -= EndRotate;
        }
    }

    public void SetDesign(CardBoxType type)
    {
        switch (type)
        {
            case CardBoxType.None:
                cardsItem.ForEach(data => data.SetDesign(spriteStandard));
                break;
            case CardBoxType.Standard:
                cardsItem.ForEach(data => data.SetDesign(spriteStandard));
                break;
            case CardBoxType.Priority:
                cardsItem.ForEach(data => data.SetDesign(spritePriority));
                break;
        }
    }

    public void Show()
    {
        for (int i = 0; i < cardsItem.Count; i++)
        {
            cardsItem[i].RotateReset();
            cardsItem[i].SetMove(transformStart);
            cardsItem[i].SetSize(sizeStart);
            cardsItem[i].Show();
        }
    }

    public void Hide()
    {
        cardsItem.ForEach(card => card.Hide());
    }

    public void MoveToShow(float time)
    {
        for (int i = 0; i < cardsItem.Count; i++)
        {
            cardsItem[i].MoveTo(transformsPoses[i], time);
            cardsItem[i].SizeTo(sizeStart, time);
        }
    }

    public void ShowRotate(float time)
    {
        for (int i = 0; i < cardsItem.Count; i++)
        {
            cardsItem[i].Rotate(time);
        }
    }

    #region Output

    public event Action OnEndMove;
    public event Action OnEndRotate;

    private void EndMove()
    {
        OnEndMove?.Invoke();
    }

    private void EndRotate()
    {
        OnEndRotate?.Invoke();
    }

    #endregion

    [Serializable]
    private class CardPseudoItem
    {
        [SerializeField] private Image imageCard;

        private Tween tweenScaleRotate;
        private Tween tweenSizeDelta;
        private Tween tweenMove;

        #region Design

        public void SetDesign(Sprite sprite)
        {
            imageCard.sprite = sprite;
        }

        #endregion

        #region Rotate

        public void RotateReset()
        {
            tweenScaleRotate?.Kill();

            imageCard.transform.localScale = Vector3.one;
        }

        public void Rotate(float time)
        {
            tweenScaleRotate?.Kill();

            tweenScaleRotate = imageCard.transform.DOScaleX(0, time).OnComplete(() =>
            {
                OnEndRotate?.Invoke();
            });
        }

        #endregion

        #region SizeDelta

        public void SetSize(Vector2 vector)
        {
            tweenSizeDelta?.Kill();

            imageCard.rectTransform.sizeDelta = vector;
        }

        public void SizeTo(Vector2 vector, float time)
        {
            tweenSizeDelta.Kill();

            tweenSizeDelta = imageCard.rectTransform.DOSizeDelta(vector, time);
        }

        #endregion

        #region Move

        public void MoveTo(Transform transform, float time)
        {
            tweenMove?.Kill();

            tweenMove = imageCard.transform.DOLocalMove(transform.localPosition, time).OnComplete(() =>
            {
                OnEndMove?.Invoke();
            });
        }

        public void SetMove(Transform transform)
        {
            tweenMove?.Kill();

            imageCard.rectTransform.localPosition = transform.localPosition;
        }

        #endregion

        #region Activate

        public void Hide()
        {
            imageCard.gameObject.SetActive(false);
        }

        public void Show()
        {
            imageCard.gameObject.SetActive(true);
        }

        #endregion



        #region Output

        public event Action OnEndMove;
        public event Action OnEndRotate;

        #endregion
    }
}
