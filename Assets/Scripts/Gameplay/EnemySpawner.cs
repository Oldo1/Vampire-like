using Assets.Scripts.ObjectPool;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class EnemySpawner : IInitializable
    {
        public const string EnemiesParentId = "EnemiesParent";

        public bool Enabled { get; set; }

        private readonly EnemySpawnerConfig _enemySpawnerConfig;
        private readonly Transform _playerTransform;
        private readonly CoroutineStarter _coroutineStarter;
        private readonly EnemyObjectPool _enemyObjectPool;
        private readonly Transform _enemiesParent;

        public EnemySpawner(EnemySpawnerConfig enemySpawnerConfig, Transform playerTransform,
            CoroutineStarter coroutineStarter, EnemyObjectPool enemyObjectPool,
            [Inject(Id = EnemiesParentId)] Transform enemiesParent)
        {
            if (enemySpawnerConfig == null)
                throw new NullReferenceException("enemySpawnerConfig is null");

            if (playerTransform == null)
                throw new NullReferenceException("playerTransform is null");

            if (coroutineStarter == null)
                throw new NullReferenceException("coroutineStarter is null");

            if (enemyObjectPool == null)
                throw new NullReferenceException("enemyObjectPool is null");

            if (enemiesParent == null)
                throw new NullReferenceException("enemiesParent is null");

            _enemySpawnerConfig = enemySpawnerConfig;
            _playerTransform = playerTransform;
            _coroutineStarter = coroutineStarter;
            _enemyObjectPool = enemyObjectPool;
            _enemiesParent = enemiesParent;
            Enabled = true;
        }

        public void Initialize()
        {
            StartSpawn();
        }

        public void StartSpawn()
        {
            _coroutineStarter.StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            var waitForSeconds = new WaitForSeconds(_enemySpawnerConfig.SpawnRate);
            var spawnerRadius = _enemySpawnerConfig.SpawnRadius;

            while (true)
            {
                if (!Enabled)
                {
                    yield return null;
                    continue;
                }

                var playerPosition = _playerTransform.position;
                var unitCircle = UnityEngine.Random.insideUnitCircle.normalized * spawnerRadius;
                var spawnPosition = playerPosition + new Vector3(unitCircle.x, 0, unitCircle.y);
                _enemyObjectPool.Get(spawnPosition, _enemiesParent);
                yield return waitForSeconds;
            }
        }
    }
}
