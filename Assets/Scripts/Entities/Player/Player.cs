using UnityEngine;
using System;
using Zenject;
using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Gameplay;
using Assets.Scripts.Configs.Items;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour, IInitializable
    {
        private PlayerInput _playerInput;
        private Health _health;
        private Level _level;
        private CrystalCollector _crystallCollector;
        private ItemInfo _startWeaponInfo;
        private Inventory _inventory;
        private UpgradeCardsContainer _upgradeCardContainer;
        private Animator _animator;

        public event Action<float> OnIncreaseLevelProgress
        {
            add { _level.OnIncreaseLevelProgress += value; }
            remove { _level.OnIncreaseLevelProgress -= value; }
        }

        public event Action<int> OnLevelUp
        {
            add { _level.OnLevelUp += value; }
            remove { _level.OnLevelUp -= value; }
        }

        [Inject]
        private void Construct(Level levelController, CrystalCollector crystalCollector, PlayerInput playerInput,
            ItemInfo startWeaponInfo, Inventory inventory, UpgradeCardsContainer upgradeCardContainer)
        {
            _level = levelController;
            _crystallCollector = crystalCollector;
            _playerInput = playerInput;
            _startWeaponInfo = startWeaponInfo;
            _inventory = inventory;
            _upgradeCardContainer = upgradeCardContainer;
        }

        public void Initialize()
        {
            _crystallCollector.OnCollect += OnCollect;
            var startItem = _upgradeCardContainer.GetUpgradeCard(_startWeaponInfo.Id).Item;
            transform.position = new Vector3(0, 0.5f, 0);
            _inventory.AddItem(startItem);
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (_playerInput != null)
            {
                var isMoving = _playerInput.GetMoveInput() != Vector2.zero;
                _animator.SetBool("IsMoving", isMoving);
            }
        }

        private void OnCollect(Crystal crystal)
        {
            _level.IncreaseLevelProgress(crystal.XpToAdd);
        }

        public void EnableCrystallCollector(bool enable)
        {
            _crystallCollector.enabled = enable;
        }

        public void EnableInput(bool enable)
        {
            if (enable)
                _playerInput.Enable();
            else
                _playerInput.Disable();
        }

        private void OnDestroy()
        {
            _crystallCollector.OnCollect -= OnCollect;
        }
    }
}
