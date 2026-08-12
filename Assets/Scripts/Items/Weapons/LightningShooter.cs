using Assets.Scripts.Configs.Items.Weapons;
using Assets.Scripts.Entities.Projectiles;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons
{
    public class LightningShooter : Weapon
    {
        public event Action<Projectile> OnShoot;

        private MonoBehaviour _shooter;
        private GameObjectPool<Lightning> _projectileObjectPool;
        private EnemyDetection _enemyDetection;
        private LightningShooterConfig _config;

        private int _projectlesCount;
        private float _cooldown;
        private float _radius;

        public LightningShooter(MonoBehaviour shooter, LightningObjectPool projectileObjectPool, EnemyDetection enemyDetection, LightningShooterConfig config) : base(config)
        {
            Debug.Log("LightningShooter проинициализировался");
            _shooter = shooter;
            _projectileObjectPool = projectileObjectPool;
            _enemyDetection = enemyDetection;
            _cooldown = config.Cooldown;
            _radius = config.Radius;
            _projectlesCount = 1;
            _config = config;
        }

        private IEnumerator ShootRoutine(Collider[] enemies, int foundEnemiesCount)
        {
            for (var i = 0; i < _projectlesCount + GameParameters.ProjectileCount; i++)
            {
                var target = enemies[i % foundEnemiesCount];
                var targetPosition = target.transform.position;
                var lighting = _projectileObjectPool.Get(targetPosition);
                var damage = _config.Damage * GameParameters.DamageMultiplier;
                var speed = _config.Speed * GameParameters.SpeedMultiplier;

                lighting.Launch(target.transform, damage, speed);

                OnShoot?.Invoke(lighting);

                if (i != _projectlesCount + GameParameters.ProjectileCount - 1)
                    yield return new WaitForSeconds(0.25f);
            }
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

        public override void StartShooting()
        {
            _shooter.StartCoroutine(ShootingRoutine());
        }

        public override void Upgrade()
        {
            _projectlesCount += 1;
        }
    }
}
