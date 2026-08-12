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
    }
}
