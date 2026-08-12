using System;
using Assets.Scripts.GameStates;
using Zenject;

namespace Assets.Scripts
{
    public class OnLevelUpManager : IInitializable, IDisposable
    {
        private readonly Player _player;
        private readonly GameStateMachine _stateMachine;
        private readonly UpgradeMenu _upgradeMenu;

        public OnLevelUpManager(Player player, GameStateMachine stateMachine, UpgradeMenu upgradeMenu)
        {
            _player = player;
            _stateMachine = stateMachine;
            _upgradeMenu = upgradeMenu;
        }

        public void Initialize()
        {
            _player.OnLevelUp += OnLevelUp;
        }

        private void OnLevelUp(int currentLevel)
        {
            if (_upgradeMenu.HasAvailableItems)
             _stateMachine.SwitchState<UpgradeSelectionState>();
        }

        public void Dispose()
        {
            _player.OnLevelUp -= OnLevelUp;
        }
    }
}
