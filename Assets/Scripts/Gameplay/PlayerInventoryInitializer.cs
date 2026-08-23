using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class PlayerInventoryInitializer : IInitializable
    {
        private readonly Player _player;
        private readonly Inventory _inventory;
        private readonly UpgradeCardsContainer _upgradeCardsContainer;

        public PlayerInventoryInitializer(Player player, Inventory inventory,
            UpgradeCardsContainer upgradeCardsContainer)
        {
            _player = player;
            _inventory = inventory;
            _upgradeCardsContainer = upgradeCardsContainer;
        }

        public void Initialize()
        {
            var startItem = _upgradeCardsContainer.GetUpgradeCard(_player.StartWeaponId).Item;
            _inventory.AddItem(startItem);
        }
    }
}
