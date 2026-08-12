using TMPro;
using UnityEngine;

namespace Assets.Scripts.GameStates
{
    public class UpgradeSelectionState : IGameState
    {
        private readonly Player _player;
        private readonly UpgradeMenu _upgradeMenu;
        private readonly GameObject _panel;
        private readonly Joystick _joystick;
        private readonly CrystalsMover _crystalsMover;
        private readonly EnemySpawner _enemySpawner;
        private readonly ProjectilesMover _projectilesMover;
        private readonly EnemyMover _enemyMover;

        public UpgradeSelectionState(Player player, UpgradeMenu upgradeMenu, GameObject panel, Joystick joystick,
            CrystalsMover crystalsMover, EnemySpawner enemySpawner, ProjectilesMover projectilesMover, EnemyMover enemyMover)
        {
            _player = player;
            _upgradeMenu = upgradeMenu;
            _panel = panel;
            _joystick = joystick;
            _crystalsMover = crystalsMover;
            _enemySpawner = enemySpawner;
            _projectilesMover = projectilesMover;
            _enemyMover = enemyMover;
        }

        public void Enter()
        {
            _player.EnableInput(false);
            _player.EnableCrystallCollector(false);
            _upgradeMenu.gameObject.SetActive(true);
            _panel.SetActive(true);
            _joystick.enabled = false;
            _crystalsMover.Enabled = false;
            _enemySpawner.Enabled = false;
            _projectilesMover.Enabled = false;
            _enemyMover.Enabled = false;
            Time.timeScale = 0;
        }

        public void Exit()
        {
            _player.EnableInput(true);
            _player.EnableCrystallCollector(true);
            _upgradeMenu.gameObject.SetActive(false);
            _panel.SetActive(false);
            _joystick.enabled = true;
            _crystalsMover.Enabled = true;
            _enemySpawner.Enabled = true;
            _projectilesMover.Enabled = true;
            _enemyMover.Enabled = true;
            Time.timeScale = 1;
        }
    }
}
