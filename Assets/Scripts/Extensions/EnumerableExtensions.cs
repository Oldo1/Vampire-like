using Assets.Scripts.Items;
using Assets.Scripts.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool ContainsItem(this IEnumerable<Slot> slots, Item item)
        {
            foreach (Slot slot in slots)
            {
                if (slot != null && slot.Item != null && slot.Item.ItemInfo.Id == item.ItemInfo.Id)
                    return true;
            }
            return false;
        }

        public static int GetRandomUniqueItems<T>(this IEnumerable<T> collection, T[] result)
        {
            var selectedItemsCount = 0;
            var processedItemsCount = 0;

            foreach (var item in collection)
            {
                processedItemsCount++;

                if (selectedItemsCount < result.Length)
                {
                    result[selectedItemsCount] = item;
                    selectedItemsCount++;
                    continue;
                }

                var randomIndex = UnityEngine.Random.Range(0, processedItemsCount);

                if (randomIndex < result.Length)
                    result[randomIndex] = item;
            }

            return selectedItemsCount;
        }
    }
}
