using Assets.Scripts.Entities.Projectiles;
using Assets.Scripts.Items.Weapons;
using UnityEngine;

namespace Assets.Scripts.Configs.Items.Weapons
{
    public class DamageAuraSpawner : Weapon
    {
        private readonly Player _shooter;
        private readonly DamageAura.Factory _damageAuraFactory;
        private readonly float _cooldown;

        private float _radius;
        private float _damage;

        public DamageAuraSpawner(Player shooter, EnemyDetection enemyDetection, DamageAuraConfig config, DamageAura.Factory damageAuraFactory) : base(config)
        {
            _shooter = shooter;
            _damage = config.Damage;
            _radius = config.Radius;
            _cooldown = config.Cooldown;
            _damageAuraFactory = damageAuraFactory;

        }

        public override void Fire()
        {
            var damageAura = _damageAuraFactory.Create();
            var transform = damageAura.transform;
            transform.position = _shooter.transform.position;
            transform.SetParent(_shooter.transform, false);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            damageAura.StartDealDamage(_cooldown, _radius, _damage);
        }

        public override void Upgrade()
        {
            _radius *= 1.15f;
            _damage *= 1.05f;
        }
    }
}
