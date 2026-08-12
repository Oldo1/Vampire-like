using Assets.Scripts.Items;
using Assets.Scripts.Items.Boosters;
using Assets.Scripts.Items.Weapons;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class Inventory : MonoBehaviour
    {
        private BoosterSlots _boostersSlots;
        private WeaponSlots _weaponsSlots;

        public event Action<Item> OnAdd;
        public event Action<Type> OnSlotFilled;

        public IEnumerable<Slot> BoostersSlots => _boostersSlots.Value;
        public IEnumerable<Slot> WeaponsSlots => _weaponsSlots.Value;

        [Inject]
        private void Construct(BoosterSlots boosterSlots, WeaponSlots weaponSlots)
        {
            _boostersSlots = boosterSlots;
            _weaponsSlots = weaponSlots;
        }

        public bool Contains(Item item)
        {
            return _boostersSlots.Contains(item) || _weaponsSlots.Contains(item);
        }

        public void AddItem<T>(T item) where T : Item
        {
            if (item is Weapon weapon)
                AddItem(weapon, _weaponsSlots);
            else if (item is Booster booster)
                AddItem(booster, _boostersSlots);
            else
                throw new ArgumentException("Unsupported item type");
        }

        private void AddItem<T>(T item, Slots<T> slots) where T : Item
        {
            slots.Add(item);
            OnAdd?.Invoke(item);

            if (slots.IsFilled)
                OnSlotFilled?.Invoke(typeof(T));
        }
    }
}
