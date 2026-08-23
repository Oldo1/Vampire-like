using Assets.Scripts.Configs.Items;
using System;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public abstract class Item
    {
        public event Action<int> OnLevelUp;

        public ItemInfo ItemInfo { get; }

        public event Action OnMaxLevelReached;

        public int CurrentLevel { get; private set; }
        public bool MaxLevelIsReached => CurrentLevel == ItemInfo.MaxLevel;

        public Item(ItemInfo itemInfo)
        {
            ItemInfo = itemInfo;
        }

        public void IncreaseCurrentLevel()
        {
            if (MaxLevelIsReached == false)
            {
                CurrentLevel++;
                if (MaxLevelIsReached)
                    OnMaxLevelReached?.Invoke();
                OnLevelUp?.Invoke(CurrentLevel);
            }
            else
                Debug.LogWarning("max level reached");
        }

        public abstract void Upgrade();
    }
}
