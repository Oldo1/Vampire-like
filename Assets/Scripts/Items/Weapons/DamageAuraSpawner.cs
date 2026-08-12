using Assets.Scripts.Entities.Projectiles;
using Assets.Scripts.Items.Weapons;
using UnityEngine;

namespace Assets.Scripts.Configs.Items.Weapons
{
    public class DamageAuraSpawner : Weapon
    {
        private readonly MonoBehaviour _shooter;
        private readonly DamageAura.Factory _damageAuraFactory;

        private float _cooldown;
        private float _radius;
        private float _damage;

        public DamageAuraSpawner(MonoBehaviour shooter, EnemyDetection enemyDetection, DamageAuraConfig config, DamageAura.Factory damageAuraFactory) : base(config)
        {
            _shooter = shooter;
            _damage = config.Damage;
            _radius = config.Radius;
            _cooldown = config.Cooldown;
            _damageAuraFactory = damageAuraFactory;

        }

        public override void StartShooting()
        {
            var damageAura = _damageAuraFactory.Create();
            damageAura.transform.position = _shooter.transform.position;
            damageAura.transform.SetParent(_shooter.transform, false);
            damageAura.transform.localPosition = Vector3.zero;
            damageAura.transform.localRotation = Quaternion.identity;
            damageAura.StartDealDamage(_cooldown, _radius, _damage);
        }

        public override void Upgrade()
        {
            _radius *= 1.15f;
            _damage *= 1.05f;
        }
    }
}
