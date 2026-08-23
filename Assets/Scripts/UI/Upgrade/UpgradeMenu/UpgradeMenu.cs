using System.Collections.Generic;
using Zenject;
using UnityEngine;
using System;
using Assets.Scripts.Gameplay;
using Assets.Scripts.Extensions;

public class UpgradeMenu : MonoBehaviour, IInitializable
{
    private const int MaxVisibleCards = 3;

    public event Action<UpgradeCard> OnCardSelect;

    public IEnumerable<UpgradeCard> AvailableCards => _availableCards.AvailableCards;
    public bool HasAvailableItems => _availableCards.HasCards;

    private UpgradeCardsContainer _availableCards;
    private readonly UpgradeCard[] _cardsToShow = new UpgradeCard[MaxVisibleCards];



    [Inject]
    private void Construct(UpgradeCardsContainer availableCards)
    {
        _availableCards = availableCards;
    }

    public void Initialize()
    {
        HideAllCards();
        OnSelectCardSubscribe();
    }

    public void OnSelectCardSubscribe()
    {
        foreach (var card in _availableCards.AllCards)
            card.OnCardSelect += OnCardSelectMethod;
    }

    private void OnSelectCardUnsubscribe()
    {
        foreach (var card in _availableCards.AllCards)
            card.OnCardSelect -= OnCardSelectMethod;
    }

    private void OnCardSelectMethod(UpgradeCard selectedCard)
    {
        gameObject.SetActive(false);
        HideAllCards();
        OnCardSelect?.Invoke(selectedCard);
    }

    private void ShowUpgradeCards()
    {
        if (_availableCards.Count == 0)
        {
            Debug.LogWarning("There is not available cards");
            return;
        }
        
        var selectedCardsCount = _availableCards.AvailableCards.GetRandomUniqueItems(_cardsToShow);

        for (var i = 0; i < selectedCardsCount; i++)
            _cardsToShow[i].gameObject.SetActive(true);
    }

    public bool RemoveCard(int id)
    {
        if (!_availableCards.TryGetUpgradeCard(id, out var card))
            return false;

        card.gameObject.SetActive(false);
        return _availableCards.Remove(id);
    }

    private void HideAllCards()
    {
        foreach (var card in _availableCards.AllCards)
            card.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ShowUpgradeCards();
    }

    private void OnDestroy()
    {
        OnSelectCardUnsubscribe();
    }
}
