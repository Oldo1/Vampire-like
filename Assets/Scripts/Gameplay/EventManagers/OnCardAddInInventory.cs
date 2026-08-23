using Assets.Scripts.Items;
using Assets.Scripts.Items.Weapons;
using System;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class OnCardAddInInventory : IInitializable, IDisposable
    {
        private readonly Inventory _inventory;

        public OnCardAddInInventory(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void Initialize()
        {
            _inventory.OnAdd += OnAdd;
        }

        private void OnAdd(Item item)
        {
            if (item is Weapon weapon)
            {
                weapon.Fire();
            }
            item.IncreaseCurrentLevel();
        }

        public void Dispose()
        {
            _inventory.OnAdd -= OnAdd;
        }
    }
}
