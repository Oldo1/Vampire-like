using System;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using Assets.Scripts.Items;
using Assets.Scripts.Items.Boosters;
using Assets.Scripts.Items.Weapons;
using Assets.Scripts.UI;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class OnSlotFilledManager : IInitializable, IDisposable
    {
        private readonly Inventory _inventory;
        private readonly UpgradeMenu _upgradeMenu;
        private readonly List<int> _itemIdsToRemove;

        public OnSlotFilledManager(Inventory inventory, UpgradeMenu upgradeMenu)
        {
            _inventory = inventory;
            _upgradeMenu = upgradeMenu;
            _itemIdsToRemove = new(capacity: 16);
        }

        public void Initialize()
        {
            _inventory.OnSlotFilled += OnSlotFilled;
        }

        private void OnSlotFilled(Type slotType)
        {
            if (slotType == typeof(Booster))
                RemoveItems<Booster>(_inventory.BoostersSlots);
            else if (slotType == typeof(Weapon))
                RemoveItems<Weapon>(_inventory.WeaponsSlots);
        }

        private void AddItemsIdToRemove<T>(IEnumerable<Slot> slots) where T : Item
        {
            foreach (var card in _upgradeMenu.AvailableCards)
            {
                var item = card.Item;

                if (item is T && !slots.ContainsItem(item))
                    _itemIdsToRemove.Add(item.ItemInfo.Id);
            }
        }

        private void RemoveItems<T>(IEnumerable<Slot> slots) where T : Item
        {
            _itemIdsToRemove.Clear();
            AddItemsIdToRemove<T>(slots);

            foreach (var id in _itemIdsToRemove)
                _upgradeMenu.RemoveCard(id);
        }

        public void Dispose()
        {
            _inventory.OnSlotFilled -= OnSlotFilled;
        }


    }
}
