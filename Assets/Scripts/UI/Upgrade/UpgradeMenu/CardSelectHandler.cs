using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class CardSelectHandler : MonoBehaviour, IPointerClickHandler
    {
        public event Action<UpgradeCard> OnCardSelect;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (TryGetComponent<UpgradeCard>(out var selectedCard))
            {
                OnCardSelect?.Invoke(selectedCard);
                Debug.Log(selectedCard.ItemInfo.Name);
            }
        }
    }
}
