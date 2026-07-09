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

    private void SendBox(CardBoxType boxType)
    {
        var cardTypes = HelperCardBoxType.GetCards(boxType, 5);

        var result = new List<CardOpenResult>();

        foreach (var type in cardTypes)
        {
            var card = _storeCardProvider.GetRandomCard(type);

            if (card == null)
                continue;

            bool duplicate = _storeCardProvider.IsCardOwned(card);

            if (!duplicate)
            {
                _storeCardProvider.OpenCard(
                    card.Type,
                    card.Page,
                    card.Index);
            }

            result.Add(new CardOpenResult(card, duplicate));
        }

        OnBuyCards?.Invoke(result);
    }

    #region Output

    public event Action<List<CardOpenResult>> OnBuyCards;

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
