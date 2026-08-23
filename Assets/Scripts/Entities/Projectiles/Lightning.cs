using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Entities.Projectiles
{
    public class Lightning : Projectile, IAttacker
    {
        private float _damage;
        private ProjectileDestroyer _projectileDestroyer;
        private LightningObjectPool _objectPool;
        private ParticleSystem[] _effects;

        [Inject]
        private void Construct(ProjectileDestroyer projectileDestroyer, LightningObjectPool objectPool)
        {
            _projectileDestroyer = projectileDestroyer;
            _objectPool = objectPool;
        }

        private void Awake()
        {
            _effects = GetComponentsInChildren<ParticleSystem>();
        }

        public void Attack(IDamageable target)
        {
            target.TakeDamage(_damage);
        }

        public override void Launch(Transform target, float damage, float speed)
        {
            if (target == null)
                throw new System.ArgumentNullException(nameof(target));
            if (damage < 0)
                throw new System.ArgumentOutOfRangeException(nameof(damage));
            if (speed < 0)
                throw new System.ArgumentOutOfRangeException(nameof(speed));

            _damage = damage;

            if (target.TryGetComponent<IDamageable>(out var damageable))
                Attack(damageable);

            StartCoroutine(ReleaseWhenEffectFinished());
        }

        private IEnumerator ReleaseWhenEffectFinished()
        {
            foreach (var effect in _effects)
            {
                effect.Clear(true);
                effect.Play(true);
            }

            yield return null;

            while (IsEffectAlive())
                yield return null;

            if (isActiveAndEnabled)
                _objectPool.Release(this);
        }

        private bool IsEffectAlive()
        {
            foreach (var effect in _effects)
            {
                if (effect != null && effect.IsAlive(true))
                    return true;
            }

            return false;
        }

        public class Factory : PlaceholderFactory<Lightning>
        {
        }
    }
}
