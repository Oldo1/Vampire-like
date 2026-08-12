using Assets.Scripts.Configs.Items;
using Assets.Scripts.Items.Weapons;
using Assets.Scripts.ObjectPool;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class FireBallShooter : Weapon
    {
        public event Action<Projectile> OnShoot;

        private readonly FireBallShooterConfig _config;
        private readonly MonoBehaviour _shooter;
        private readonly EnemyDetection _enemyDetection;
        private readonly FireBallObjectPool _projectileObjectPool;

        private float _cooldown;
        private float _projectlesCount;
        private float _damageMultiplier;
        private float _radius;

        public FireBallShooter(MonoBehaviour shooter, FireBallObjectPool projectileObjectPool, EnemyDetection enemyDetection, FireBallShooterConfig config) : base(config)
        {
            if (shooter == null)
                throw new NullReferenceException("shooter is null");
            if (projectileObjectPool == null)
                throw new NullReferenceException("projectileObjectPool is null");

            _shooter = shooter;
            _projectileObjectPool = projectileObjectPool;
            _cooldown = config.Cooldown;
            _enemyDetection = enemyDetection;
            _projectlesCount = 1;
            _damageMultiplier = 1;
            _config = config;
            _radius = config.Radius;
        }


        public override void StartShooting()
        {
            _shooter.StartCoroutine(ShootingRoutine());
        }

        public override void Upgrade()
        {
            _damageMultiplier += _config.DamageMultiplierAmount;
            _projectlesCount += _config.ProjectileAmount;
        }

        private IEnumerator ShootingRoutine()
        {
            var waitForCooldown = new WaitForSeconds(_cooldown * GameParameters.CooldownMultiplier);
            var waitForFixedUpdate = new WaitForFixedUpdate();

            while (true)
            {
                var position = _shooter.transform.position;
                if (_enemyDetection.TryGetEnemiesInRadius(position, _radius, out var enemies, out var foundEnemiesCount))
                {
                    yield return _shooter.StartCoroutine(ShootRoutine(enemies, foundEnemiesCount));
                    yield return waitForCooldown;
                }
                yield return waitForFixedUpdate;
            }
        }

        private IEnumerator ShootRoutine(Collider[] enemies, float foundEnemiesCount)
        {
            for (var i = 0; i < _projectlesCount + GameParameters.ProjectileCount; i++)
            {
                var target = FindClosestEnemy(enemies, foundEnemiesCount);

                if (target == null)
                    break;

                var shooterPosition = _shooter.transform.position;
                var projectile = _projectileObjectPool.Get(shooterPosition);
                var damage = _config.Damage * _damageMultiplier * GameParameters.DamageMultiplier;
                var speed = _config.Speed * GameParameters.SpeedMultiplier;

                projectile.Launch(target, damage, speed);
                OnShoot?.Invoke(projectile);

                if (i != _projectlesCount + GameParameters.ProjectileCount - 1)
                    yield return new WaitForSeconds(0.25f);
            }
        }

        private Transform FindClosestEnemy(Collider[] enemies, float foundEnemiesCount)
        {
            Transform result = null;
            var minDistance = float.MaxValue;

            for (var i = 0; i < foundEnemiesCount; i++)
            {
                var currentEnemy = enemies[i].transform;
                var enemyPosition = currentEnemy.position;
                var distance = Vector3.Distance(enemyPosition, _shooter.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = currentEnemy;
                }
            }
            return result;
        }
    }
}


