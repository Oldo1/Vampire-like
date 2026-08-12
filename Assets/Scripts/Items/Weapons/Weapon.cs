using Assets.Scripts.Configs.Items;

namespace Assets.Scripts.Items.Weapons
{
    public abstract class Weapon : Item
    {
        public Weapon(ItemInfo itemInfo) : base(itemInfo)
        {
        }

        public abstract void StartShooting();
    }
}
