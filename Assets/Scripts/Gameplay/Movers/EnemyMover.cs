using Assets.Scripts.ObjectPool;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class EnemyMover : ITickable
    {
        public const string RotationSpeedId = "EnemyRotationSpeed";

        public bool Enabled { get; set; }

        private readonly Transform _playerTransform;
        private readonly EnemyObjectPool _enemyObjectPool;
        private readonly MovementRotator _movementRotator;
        private readonly float _rotationSpeed;

        public EnemyMover(Transform transform, EnemyObjectPool enemyObjectPool, MovementRotator movementRotator,
            [Inject(Id = RotationSpeedId)] float rotationSpeed)
        {
            if (transform == null)
                throw new NullReferenceException("transform is null");
            if (enemyObjectPool == null)
                throw new NullReferenceException("enemyObjectPool is null");
            if (movementRotator == null)
                throw new NullReferenceException("movementRotator is null");

            _playerTransform = transform;  
            _enemyObjectPool = enemyObjectPool;
            _movementRotator = movementRotator;
            _rotationSpeed = rotationSpeed;
            Enabled = true;
        }

        public void Tick()
        {
            if (!Enabled) return;

            var playerPosition = _playerTransform.position;
            foreach (var enemy in _enemyObjectPool.ActiveObjects)
            {
                var enemyPosition = enemy.transform.position;
                var direction = (playerPosition - enemyPosition).normalized;
                _movementRotator.RotateTowardsMovement(enemy.Model, direction, _rotationSpeed);
                enemy.Move(direction);
            }
        }
    }
}
