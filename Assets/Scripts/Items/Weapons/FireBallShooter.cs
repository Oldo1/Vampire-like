using Assets.Scripts.Configs.Items;
using Assets.Scripts.Items.Weapons;
using Assets.Scripts.ObjectPool;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class FireBallShooter : ProjectileShooter
    {
        public event Action<Projectile> OnShoot;

        private readonly FireBallShooterConfig _config;
        private readonly FireBallObjectPool _projectileObjectPool;

        public FireBallShooter(Player shooter, FireBallObjectPool projectileObjectPool, EnemyDetection enemyDetection, FireBallShooterConfig config)
            : base(config, shooter, enemyDetection, config.Cooldown, config.Radius)
        {
            _projectileObjectPool = projectileObjectPool ?? throw new ArgumentNullException(nameof(projectileObjectPool));
            _config = config;
        }

        public override void Upgrade()
        {
            IncreaseDamageMultiplier(amount: _config.DamageMultiplierAmount);
            IncreaseProjectileCount(amount: _config.ProjectileAmount);
        }

        protected override IEnumerator ShootRoutine(Collider[] enemies, int foundEnemyCount)
        {
            for (var i = 0; i < ProjectilesCount; i++)
            {
                var target = FindClosestEnemy(enemies, foundEnemyCount);

                if (target == null)
                    break;

                var projectile = _projectileObjectPool.Get(Shooter.transform.position);
                projectile.Launch(target, Damage, Speed);
                OnShoot?.Invoke(projectile);

                if (i != ProjectilesCount - 1)
                    yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
