using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class CardPresentationView : View
{
    [Header("CARDS")]
    [SerializeField] private List<CardPresentationUnit> cardPresentationUnits = new();
    [SerializeField] private Color colorDuplicate;

    [Header("PRESENTATION")]
    [SerializeField] private Image imageCardPresentation;

    public void Initialize()
    {
        for (int i = 0; i < cardPresentationUnits.Count; i++)
        {
            cardPresentationUnits[i].OnClickCard += ClickCard;
            cardPresentationUnits[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < cardPresentationUnits.Count; i++)
        {
            cardPresentationUnits[i].OnClickCard -= ClickCard;
            cardPresentationUnits[i].Dispose();
        }
    }

    #region CARD PRESENTATION

    public void CardPresentation(Sprite sprite)
    {
        imageCardPresentation.sprite = sprite;
    }

    #endregion

    #region CARDS

    public void SetCards(List<CardOpenResult> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cardPresentationUnits[i].SetData(cards[i]);
        }
    }

    public void Show(float time)
    {
        for (int i = 0; i < cardPresentationUnits.Count; i++)
        {
            cardPresentationUnits[i].Show(time);
        }
    }

    public void Hide()
    {
        for (int i = 0; i < cardPresentationUnits.Count; i++)
        {
            cardPresentationUnits[i].Hide();
        }
    }

    public void ShowDuplicates()
    {
        for (int i = 0; i < cardPresentationUnits.Count; i++)
        {
            cardPresentationUnits[i].ShowDuplicate(colorDuplicate);
        }
    }

    #endregion

    #region Output

    public event Action<CardOpenResult> OnClickCard;

    private void ClickCard(CardOpenResult cardOpen)
    {
        OnClickCard?.Invoke(cardOpen);
    }

    #endregion

    [Serializable]
    private class CardPresentationUnit
    {
        [SerializeField] private Button buttonUnit;
        [SerializeField] private Image imageCard;
        [SerializeField] private Transform transformCardShake;
        [SerializeField] private GameObject duplicateIcon;

        private CardOpenResult _currentCardOpen = null;

        private Tween tweenScale;

        public void Initialize()
        {
            buttonUnit.onClick.AddListener(ClickCard);
        }

        public void Dispose()
        {
            buttonUnit.onClick.RemoveListener(ClickCard);
        }

        public void Show(float time)
        {
            tweenScale?.Kill();

            transformCardShake.gameObject.SetActive(true);
            transformCardShake.localScale = new Vector3(0, 1 ,1);

            tweenScale = transformCardShake.DOScaleX(1, time);
        }

        public void Hide()
        {
            tweenScale?.Kill();

            transformCardShake.gameObject.SetActive(false);
        }

        public void SetData(CardOpenResult cardOpen)
        {
            imageCard.color = Color.white;
            imageCard.sprite = cardOpen.Card.Sprite;

            duplicateIcon.SetActive(false);

            _currentCardOpen = cardOpen;

        }

        public void ShowDuplicate(Color color)
        {
            if (_currentCardOpen == null) return;

            if (!_currentCardOpen.IsDuplicate) return;

            duplicateIcon.SetActive(true);

            transformCardShake
                .DOScale(1.1f, 0.15f)
                .OnComplete(() =>
                {
                    transformCardShake.DOScale(1f, 0.15f);
                });

            imageCard.DOColor(color, 0.2f);

            duplicateIcon
                .transform
                .DOScale(1f, 0.2f)
                .From(0f);
        }

        #region Output

        public event Action<CardOpenResult> OnClickCard;

        private void ClickCard()
        {
            if(_currentCardOpen == null) return;

            OnClickCard?.Invoke(_currentCardOpen);
        }

        #endregion
    }
}
