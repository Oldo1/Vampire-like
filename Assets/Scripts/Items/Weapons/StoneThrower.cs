using Assets.Scripts.Configs.Items.Weapons;
using Assets.Scripts.ObjectPool;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons
{
    public class StoneThrower : ProjectileShooter
    {
        public event Action<Projectile> OnShoot;

        private readonly StoneObjectPool _stoneObjectPool;

        public StoneThrower(Player shooter, StoneObjectPool stoneObjectPool, EnemyDetection enemyDetection, StoneThrowerConfig config)
            : base(config, shooter, enemyDetection, config.Cooldown, config.Radius)
        {
            _stoneObjectPool = stoneObjectPool ?? throw new ArgumentNullException(nameof(stoneObjectPool));
        }

        public override void Upgrade()
        {
        }

        protected override IEnumerator ShootRoutine(Collider[] enemies, int foundEnemiesCount)
        {
            for (var i = 0; i < ProjectilesCount; i++)
            {
                var target = enemies[i % foundEnemiesCount];
                var projectile = _stoneObjectPool.Get(Shooter.transform.position);
                projectile.Launch(target.transform, Damage, Speed);
                OnShoot?.Invoke(projectile);

                if (i != ProjectilesCount - 1)
                    yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
