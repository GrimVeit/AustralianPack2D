using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsOrchestrationModel
{
    private ICardsGameSpawnerListener _spawnListener;

    private List<IGameCard> _cards;

    private IGameCard _firstCard;
    private IGameCard _secondCard;

    private bool _lockInput;
    private bool _isActive = false;

    private readonly float _compareDelay = 0.4f;
    private readonly float _hideDelayReturn = 0.3f;
    private readonly float _hideDelayHide = 0.5f;

    private IEnumerator processCoroutine;

    private readonly ISoundProvider _soundProvider;

    public CardsOrchestrationModel(ICardsGameSpawnerListener spawnListener, ISoundProvider soundProvider)
    {
        _spawnListener = spawnListener;

        _spawnListener.OnSpawnedCards += OnCardsSpawned;
        _spawnListener.OnDestroyCard += DestroyCard;
        _soundProvider = soundProvider;
    }

    public void Dispose()
    {
        if (_spawnListener != null)
        {
            _spawnListener.OnSpawnedCards -= OnCardsSpawned;
            _spawnListener.OnDestroyCard -= DestroyCard;
        }

        UnsubscribeCards();
    }

    public void ActivateInteractive()
    {
        _isActive = true;
    }

    public void DeactivateInteractive()
    {
        _isActive = false;
    }

    public void ShowCards()
    {
        if (_cards == null) return;

        _cards.ForEach(c => c.Show());
    }

    public void HideCards()
    {
        if (_cards == null) return;

        _cards.ForEach(c => c.Hide());
    }

    private void OnCardsSpawned(IReadOnlyList<IGameCard> cards)
    {
        UnsubscribeCards();

        _cards = cards.ToList();

        foreach (var card in _cards)
        {
            card.OnChooseCard += OnCardChosen;
            card.ActivateInteraction();
            card.Hide(); // reset состояния
        }

        ResetState();
    }

    private void DestroyCard(IGameCard card)
    {
        card.OnChooseCard -= OnCardChosen;

        _cards.Remove(card);
    }

    private void UnsubscribeCards()
    {
        if (_cards == null)
            return;

        foreach (var card in _cards)
        {
            card.OnChooseCard -= OnCardChosen;
        }
    }

    private void ResetState()
    {
        _firstCard = null;
        _secondCard = null;
        _lockInput = false;
    }

    private void OnCardChosen(IGameCard card)
    {
        if (!_isActive) return;

        if (_lockInput) return;

        if (_firstCard == null)
        {
            SelectFirst(card);
            return;
        }

        if (_firstCard == card)
            return;

        SelectSecond(card);
    }

    private void SelectFirst(IGameCard card)
    {
        _firstCard = card;

        _soundProvider.PlayOneShot("ChooseCardGame");

        _firstCard.Show();
        _firstCard.DeactivateInteraction();
    }

    private void SelectSecond(IGameCard card)
    {
        _secondCard = card;

        _secondCard.Show();
        _secondCard.DeactivateInteraction();

        _lockInput = true;

        Coroutines.Start(CompareRoutine());
    }

    private IEnumerator CompareRoutine()
    {
        if (_firstCard.IdPair == _secondCard.IdPair)
        {
            _soundProvider.PlayOneShot("ChooseCardGame_GOOD");

            yield return new WaitForSeconds(_compareDelay);

            HandleMatch();
        }
        else
        {
            _soundProvider.PlayOneShot("ChooseCardGame_BAD");

            yield return new WaitForSeconds(_compareDelay);

            HandleMismatch();
        }
    }

    private void HandleMatch()
    {
        _firstCard.Shake();
        _secondCard.Shake();

        _firstCard.Effect();
        _secondCard.Effect();

        OnAddMatch?.Invoke();
        OnAddMove?.Invoke();

        Coroutines.Start(HideRoutine());
    }

    private void HandleMismatch()
    {
        _firstCard.Shake();
        _secondCard.Shake();

        OnAddMove?.Invoke();

        Coroutines.Start(ReturnRoutine());
    }

    private IEnumerator ReturnRoutine()
    {
        yield return new WaitForSeconds(_hideDelayReturn);

        _firstCard.Hide();
        _firstCard.ActivateInteraction();

        _secondCard.Hide();
        _secondCard.ActivateInteraction();

        ResetState();
    }

    private IEnumerator HideRoutine()
    {
        yield return new WaitForSeconds(_hideDelayHide);

        _firstCard.HideDestroy();
        _secondCard.HideDestroy();

        ResetState();
    }

    #region Output

    public event Action OnAddMove;
    public event Action OnAddMatch;

    #endregion
}
