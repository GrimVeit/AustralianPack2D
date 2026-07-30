using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardVisualView : View
{
    [SerializeField] private List<CardVisualPage> pages = new();

    public void Initialize()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].OnCardClick += CardClick;
            pages[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].OnCardClick -= CardClick;
            pages[i].Dispose();
        }
    }

    public void OpenCard(Card card, bool isNew)
    {
        pages[card.Page].OpenCard(card, isNew);
    }

    public void CloseCard(Card card)
    {
        pages[card.Page].CloseCard(card);
    }

    #region Output

    public event Action<Card> OnCardClick;

    private void CardClick(Card card)
    {
        OnCardClick?.Invoke(card);
    }

    #endregion

    [Serializable]
    private class CardVisualPage
    {
        [SerializeField] private string name;
        [SerializeField] private List<CardVisualElement> cardsElements = new();

        public void Initialize()
        {
            for (int i = 0; i < cardsElements.Count; i++)
            {
                cardsElements[i].OnCardClick += CardClick;
                cardsElements[i].Initialize();
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < cardsElements.Count; i++)
            {
                cardsElements[i].OnCardClick -= CardClick;
                cardsElements[i].Dispose();
            }
        }

        public void OpenCard(Card card, bool isNew)
        {
            cardsElements[card.Index].OpenCard(card, isNew);
        }

        public void CloseCard(Card card)
        {
            cardsElements[card.Index].CloseCard(card);
        }

        #region Output

        public event Action<Card> OnCardClick;

        private void CardClick(Card card)
        {
            OnCardClick?.Invoke(card);
        }

        #endregion
    }

    [Serializable]
    private class CardVisualElement
    {
        [SerializeField] private Button buttonElement;
        [SerializeField] private Image imageElement;
        [SerializeField] private GameObject objectNew;

        private Card _card;
        private bool isOpen = false;

        public void Initialize()
        {
            buttonElement.onClick.AddListener(CardClick);
        }

        public void Dispose()
        {
            buttonElement.onClick.RemoveListener(CardClick);
        }

        public void OpenCard(Card card, bool isNew)
        {
            _card = card;

            imageElement.gameObject.SetActive(true);
            imageElement.sprite = _card.Sprite;

            isOpen = true;

            if (isNew)
            {
                objectNew.SetActive(true);
            }
            else
            {
                objectNew.SetActive(false);
            }
        }

        public void CloseCard(Card card)
        {
            _card = card;

            isOpen = false;

            imageElement.gameObject.SetActive(false);
        }

        #region Output

        public event Action<Card> OnCardClick;

        private void CardClick()
        {
            if (!isOpen) return;

            OnCardClick?.Invoke(_card);
        }

        #endregion
    }
}
