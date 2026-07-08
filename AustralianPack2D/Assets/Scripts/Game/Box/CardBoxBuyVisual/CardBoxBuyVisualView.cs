using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBoxBuyVisualView : View
{
    [SerializeField] private List<CardBoxBuyVisualItem> items = new();

    private readonly Dictionary<CardBoxType, CardBoxBuyVisualItem> _boxItems = new();

    public void Initialize()
    {
        for (int i = 0; i < items.Count; i++)
        {
            _boxItems.Add(items[i].Type, items[i]);
        }

        for (int i = 0; i < _boxItems.Count; i++)
        {
            items[i].OnClickBuy += ClickBuy;
            items[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < _boxItems.Count; i++)
        {
            items[i].OnClickBuy -= ClickBuy;
            items[i].Dispose();
        }
    }

    #region Output

    public event Action<CardBoxType, int> OnClickBuy;

    private void ClickBuy(CardBoxType type, int cost)
    {
        OnClickBuy?.Invoke(type, cost);
    }

    #endregion

    [Serializable]
    private class CardBoxBuyVisualItem
    {
        public CardBoxType Type => cardBoxType;

        [SerializeField] private Button buttonBuy;
        [SerializeField] private int cost;
        [SerializeField] private CardBoxType cardBoxType;

        public void Initialize()
        {
            buttonBuy.onClick.AddListener(ClickBuy);
        }

        public void Dispose()
        {
            buttonBuy.onClick.RemoveListener(ClickBuy);
        }

        #region Output

        public event Action<CardBoxType, int> OnClickBuy;

        private void ClickBuy()
        {
            OnClickBuy?.Invoke(cardBoxType, cost);
        }

        #endregion
    }
}
