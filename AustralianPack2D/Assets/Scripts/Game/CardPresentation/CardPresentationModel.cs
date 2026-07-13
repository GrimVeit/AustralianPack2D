using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPresentationModel
{
    private readonly ICardBoxBuyListener _cardBoxBuyListener;

    private readonly IStoreCardProvider _storeCardProvider;

    public CardPresentationModel(ICardBoxBuyListener cardBoxBuyListener, IStoreCardProvider storeCardProvider, IStoreCardListener storeCardListener)
    {
        _cardBoxBuyListener = cardBoxBuyListener;
        _storeCardProvider = storeCardProvider;
    }

    public void Initialize()
    {
        _cardBoxBuyListener.OnSendBox += SendBox;
    }

    public void Dispose()
    {
        _cardBoxBuyListener.OnSendBox -= SendBox;
    }

    public void ClickCard(CardOpenResult cardOpen)
    {
        OnClickCard?.Invoke(cardOpen);
    }

    private void SendBox(CardBoxType boxType)
    {
        var cardTypes = HelperCardBoxType.GetCards(boxType, 5);

        var result = new List<CardOpenResult>();

        var isUniqueCount = 0;

        foreach (var type in cardTypes)
        {
            var card = _storeCardProvider.GetRandomCard(type);

            if (card == null)
                continue;

            bool duplicate = _storeCardProvider.IsCardOwned(card);

            if (!duplicate)
            {
                isUniqueCount += 1;
                _storeCardProvider.OpenCard(
                    card.Type,
                    card.Page,
                    card.Index);
            }

            result.Add(new CardOpenResult(card, duplicate));
        }

        OnGetCountUniqueCards?.Invoke(isUniqueCount);

        OnBuyCards?.Invoke(result);
    }

    #region Output

    public event Action<int> OnGetCountUniqueCards;

    public event Action<List<CardOpenResult>> OnBuyCards;

    public event Action<CardOpenResult> OnClickCard;

    #endregion
}

public class CardOpenResult
{
    public Card Card { get; }
    public bool IsDuplicate { get; }

    public CardOpenResult(Card card, bool isDuplicate)
    {
        Card = card;
        IsDuplicate = isDuplicate;
    }
}
