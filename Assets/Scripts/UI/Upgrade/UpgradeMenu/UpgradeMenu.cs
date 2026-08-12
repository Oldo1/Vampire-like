using System.Collections.Generic;
using System.Linq;
using Zenject;
using UnityEngine;
using Assets.Scripts.Extensions;
using System;
using Assets.Scripts.Items;
using Assets.Scripts.Gameplay;

public class UpgradeMenu : MonoBehaviour, IInitializable
{
    public event Action<UpgradeCard> OnCardSelect;

    public IEnumerable<Item> AvailableItems => _availableCards.Values.Select(x => x.Item);

    private UpgradeCardsContainer _availableCards;
    public bool HasAvailableItems => _availableCards.HasCards;
    

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
        foreach (var card in _availableCards.Values)
            card.OnCardSelect += OnCardSelectMethod;
    }

    private void OnSelectCardUnsubscribe()
    {
        foreach (var card in _availableCards.Values)
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
        
        var availableCardsList = _availableCards.Values.ToList();

        availableCardsList.Shuffle();

        for (var i = 0; i < Mathf.Min(3, _availableCards.Count); i++)
            availableCardsList[i].gameObject.SetActive(true);
    }

    public bool RemoveCard(int id)
    {
        _availableCards.GetUpgradeCard(id).gameObject.SetActive(false);
        return _availableCards.Remove(id);
    }

    private void HideAllCards()
    {
        foreach (var card in _availableCards.Values)
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
