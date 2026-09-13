using Assets.Scripts.ObjectPool;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class EnemiesMover : ITickable, IInitializable, IDisposable
    {
        public bool Enabled { get; set; }

        private readonly Transform _playerTransform;
        private readonly EnemyObjectPool _enemyObjectPool;
        private readonly MovementRotator _movementRotator;
        private readonly float _rotationSpeed;
        private HashSet<Enemy> _activeEnemies;

        public EnemiesMover(Transform transform, EnemyObjectPool enemyObjectPool, 
            MovementRotator movementRotator,float rotationSpeed)
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

        public void Initialize()
        {
            _activeEnemies = new HashSet<Enemy>();
            _enemyObjectPool.OnGet += OnPoolGet;
            _enemyObjectPool.OnRelease += OnPoolRelease;
        }

        private void OnPoolGet(Enemy enemy)
        {
            _activeEnemies.Add(enemy);
        }

        private void OnPoolRelease(Enemy enemy)
        {
            _activeEnemies.Remove(enemy);
        }

        public void Tick()
        {
            if (!Enabled) return;

            var playerPosition = _playerTransform.position;
            foreach (var enemy in _activeEnemies)
            {
                var enemyPosition = enemy.transform.position;
                var direction = (playerPosition - enemyPosition).normalized;
                _movementRotator.RotateTowardsDirection(enemy.Model, direction, _rotationSpeed);
                enemy.Move(direction);
            }
        }

        public void Dispose()
        {
            _enemyObjectPool.OnGet -= OnPoolGet;
            _enemyObjectPool.OnRelease -= OnPoolRelease;
        }
    }
}
