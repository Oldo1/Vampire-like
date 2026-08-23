using System;
using Assets.Scripts.GameStates;
using Assets.Scripts.Items.Boosters;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class OnCardSelectManager : IInitializable, IDisposable
    {
        private readonly UpgradeMenu _upgradeMenu;
        private readonly Inventory _inventory;
        private readonly GameStateMachine _stateMachine;

        public OnCardSelectManager(UpgradeMenu upgradeMenu, Inventory inventory, GameStateMachine stateMachine)
        {
            _upgradeMenu = upgradeMenu;
            _inventory = inventory;
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _upgradeMenu.OnCardSelect += OnCardSelect;
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
        }
    }
}
