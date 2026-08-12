using Assets.Scripts.Extensions;
using Assets.Scripts.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Slots<T> : MonoBehaviour where T : Item
    {
        private Slot[] _slots;
        private int _currentSlotIndex;

        public int Length => _slots.Length;
        public IEnumerable<Slot> Value => _slots;
        public bool IsFilled => _currentSlotIndex == _slots.Length;

        private void Awake()
        {
            _slots = GetComponentsInChildren<Slot>(true);

            if (_slots == null || _slots.Length == 0)
                throw new System.NullReferenceException("slots are empty");

            _currentSlotIndex = 0;
        }

        public void Add(Item item)
        {
            if (_currentSlotIndex == _slots.Length)
                throw new System.InvalidOperationException("slots are filled");

            if (_slots.ContainsItem(item))
                throw new System.InvalidOperationException("item already contains in slot");

            if (item is not T)
                throw new System.InvalidOperationException($"item type is not {typeof(T)}");

            _slots[_currentSlotIndex].Item = item;
            _currentSlotIndex++;
        }

        public bool Contains(Item item)
        {
            return _slots.ContainsItem(item);
        }
    }
}
