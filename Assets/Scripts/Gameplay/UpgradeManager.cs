using Assets.Scripts.Extensions;
using Assets.Scripts.GameStates;
using Assets.Scripts.Items;
using Assets.Scripts.Items.Boosters;
using Assets.Scripts.Items.Weapons;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Assets.Scripts
{
    public class UpgradeManager : IInitializable, IDisposable
    {
        private readonly UpgradeMenu _upgradeMenu;
        private readonly Inventory _inventory;
        private readonly GameStateMachine _stateMachine;

        public UpgradeManager(UpgradeMenu upgradeMenu, Inventory inventory, GameStateMachine stateMachine)
        {
            _upgradeMenu = upgradeMenu;
            _inventory = inventory;
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _upgradeMenu.OnCardSelect += OnCardSelect;
            _inventory.OnSlotFilled += OnSlotFilled;
        }

        public void OnSlotFilled(Type slotType)
        {
            if (slotType == typeof(Booster))
                RemoveItems<Booster>(_inventory.BoostersSlots);
            else if (slotType == typeof(Weapon))
                RemoveItems<Weapon>(_inventory.WeaponsSlots);
        }

        private void RemoveItems<T>(IEnumerable<Slot> slots) where T : Item
        {
            var availableItems = _upgradeMenu.AvailableItems
                .OfType<T>()
                .Where(item => !slots.ContainsItem(item))
                .ToList();

            foreach (var item in availableItems)
            {
                _upgradeMenu.RemoveCard(item.ItemInfo.Id);
            }
        }

        private void OnCardSelect(UpgradeCard upgradeCard)
        {
            var itemId = upgradeCard.ItemInfo.Id;
            var item = upgradeCard.Item;

            if (item.MaxLevelIsReached)
            {

               _upgradeMenu.RemoveCard(itemId);
                _stateMachine.ExitCurrentState();
                return;
            }

            var alreadyOwned = _inventory.Contains(item);

            if (alreadyOwned)
            {
                item.Upgrade();
                item.IncreaseCurrentLevel();
            }
            else
            {
                if (item is Booster)
                    item.Upgrade();

                _inventory.AddItem(item);
            }

            if (item.MaxLevelIsReached)
                _upgradeMenu.RemoveCard(item.ItemInfo.Id);

            _stateMachine.ExitCurrentState();
        }

        public void Dispose()
        {
            _upgradeMenu.OnCardSelect -= OnCardSelect;
            _inventory.OnSlotFilled -= OnSlotFilled;
        }
    }
}
