using Assets.Scripts.Configs.Items;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons
{
    public abstract class ProjectileShooter : Weapon
    {
        private readonly float _cooldown;
        private readonly float _radius;
        private readonly float _speed;
        private readonly float _damage;
        private readonly EnemyDetection _enemyDetection;

        protected Player Shooter { get; }
        private float _projectilesCount;
        private float _damageMultiplier;

        protected float Speed => _speed * GameParameters.SpeedMultiplier;
        protected float Damage => _damage * _damageMultiplier * GameParameters.DamageMultiplier;
        protected float ProjectilesCount => _projectilesCount + GameParameters.ProjectileCount;

        protected ProjectileShooter(
            ShooterConfig config,
            Player shooter,
            EnemyDetection enemyDetection,
            float cooldown,
            float radius) : base(config)
        {
            Shooter = shooter != null ? shooter : throw new ArgumentNullException(nameof(shooter));
            _enemyDetection = enemyDetection ?? throw new ArgumentNullException(nameof(enemyDetection));
            _speed = config.Speed;
            _damage = config.Damage;
            _cooldown = cooldown;
            _radius = radius;

            _projectilesCount = config.ProjectileAmount;
            _damageMultiplier = 1f;
        }

        protected void IncreaseProjectileCount(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _projectilesCount += amount;
        }

        protected void IncreaseDamageMultiplier(float amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _damageMultiplier += amount;
        }

        public override void Fire()
        {
            Shooter.StartCoroutine(ShootingRoutine());
        }

        private IEnumerator ShootingRoutine()
        {
            var waitForCooldown = new WaitForSeconds(_cooldown * GameParameters.CooldownMultiplier);
            var waitForFixedUpdate = new WaitForFixedUpdate();

            while (true)
            {
                var position = Shooter.transform.position;
                if (_enemyDetection.TryGetEnemiesInRadius(position, _radius, out var enemies, out var foundEnemiesCount))
                {
                    yield return Shooter.StartCoroutine(ShootRoutine(enemies, foundEnemiesCount));
                    yield return waitForCooldown;
                }

                yield return waitForFixedUpdate;
            }
        }

        protected abstract IEnumerator ShootRoutine(Collider[] enemies, int foundEnemyCount);

        protected Transform FindClosestEnemy(Collider[] enemies, int foundEnemiesCount)
        {
            Transform result = null;
            var minDistance = float.MaxValue;
            var shooterPosition = Shooter.transform.position;

            for (var i = 0; i < foundEnemiesCount; i++)
            {
                var currentEnemy = enemies[i].transform;
                var distance = Vector3.Distance(currentEnemy.position, shooterPosition);
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
