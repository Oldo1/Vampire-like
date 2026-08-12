using Assets.Scripts.Configs.Items.Weapons;
using Assets.Scripts.ObjectPool;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons
{
    public class StoneThrower : Weapon
    {
        public event Action<Projectile> OnShoot;

        private readonly StoneThrowerConfig _config;
        private readonly MonoBehaviour _shooter;
        private readonly EnemyDetection _enemyDetection;
        private readonly StoneObjectPool _stoneObjectPool;

        private float _cooldown;
        private float _projectlesCount;
        private float _damageMultiplier;
        private float _radius;

        public StoneThrower(MonoBehaviour shooter, StoneObjectPool stoneObjectPool, EnemyDetection enemyDetection, StoneThrowerConfig config) : base(config)
        {
            if (shooter == null)
                throw new NullReferenceException("shooter is null");
            if (stoneObjectPool == null)
                throw new NullReferenceException("projectileObjectPool is null");

            _shooter = shooter;
            _stoneObjectPool = stoneObjectPool;
            _cooldown = config.Cooldown;
            _enemyDetection = enemyDetection;
            _projectlesCount = 1;
            _damageMultiplier = 1;
            _radius = config.Radius;
            _config = config;
        }


        public override void StartShooting()
        {
            _shooter.StartCoroutine(ShootingRoutine());
        }

        public override void Upgrade()
        {
            /*_damageMultiplier += _config.DamageMultiplierAmount;
            _projectlesCount += _config.ProjectileAmount;*/
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

        private IEnumerator ShootRoutine(Collider[] enemies, int foundEnemiesCount)
        {
            for (var i = 0; i < _projectlesCount + GameParameters.ProjectileCount; i++)
            {
                var target = enemies[i % foundEnemiesCount];
                var shooterPosition = _shooter.transform.position;
                var projectile = _stoneObjectPool.Get(shooterPosition);
                var damage = _config.Damage * _damageMultiplier * GameParameters.DamageMultiplier;
                var speed = _config.Speed * GameParameters.SpeedMultiplier;

                projectile.Launch(target.transform, damage, speed);
                OnShoot?.Invoke(projectile);

                if (i != _projectlesCount + GameParameters.ProjectileCount - 1)
                    yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
