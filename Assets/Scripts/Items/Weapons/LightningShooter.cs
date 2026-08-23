using Assets.Scripts.Configs.Items.Weapons;
using Assets.Scripts.Entities.Projectiles;
using Assets.Scripts.ObjectPool;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons
{
    public class LightningShooter : ProjectileShooter
    {
        public event Action<Projectile> OnShoot;

        private readonly GameObjectPool<Lightning> _projectileObjectPool;

        public LightningShooter(Player shooter, LightningObjectPool projectileObjectPool, EnemyDetection enemyDetection, LightningShooterConfig config)
            : base(config, shooter, enemyDetection, config.Cooldown, config.Radius)
        {
            _projectileObjectPool = projectileObjectPool ?? throw new ArgumentNullException(nameof(projectileObjectPool));
        }

        protected override IEnumerator ShootRoutine(Collider[] enemies, int foundEnemiesCount)
        {
            for (var i = 0; i < ProjectilesCount; i++)
            {
                var target = enemies[i % foundEnemiesCount];

                if (!target.gameObject.activeSelf)
                    continue;

                var targetPosition = target.transform.position;
                var lighting = _projectileObjectPool.Get(targetPosition);
                lighting.Launch(target.transform, Damage, Speed);
                OnShoot?.Invoke(lighting);

                if (i != ProjectilesCount - 1)
                    yield return new WaitForSeconds(0.25f);
            }
        }

        public override void Upgrade()
        {
            IncreaseProjectileCount(amount: 1);
        }
    }
}
