using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Entities.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Stone : Projectile
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField, Min(0)] private float _maxHeight;
        [SerializeField, Min(0)] private float _splashDamageRadius;
        [SerializeField, Min(0)] private float _splashDamage;
        [SerializeField, Min(0)] private float _flyingTime; 

        private Transform _target;
        private EnemyDetection _enemyDetection;
        private StoneObjectPool _objectPool;
        private float _damage;

        [Inject]
        private void Construct(EnemyDetection enemyDetection, StoneObjectPool objectPool)
        {
            _enemyDetection = enemyDetection;
            _objectPool = objectPool;
        }

        public override void Launch(Transform target, float damage, float speed)
        {
            if (target == null)
                throw new System.ArgumentNullException(nameof(target));
            if (damage < 0)
                throw new System.ArgumentOutOfRangeException(nameof(damage));
            if (speed <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(speed));

            _target = target;
            _damage = damage;
            StartCoroutine(ProjectileMovement());
        }

        private IEnumerator ProjectileMovement()
        {
            var t = 0f;
            var startPosition = transform.position;
            var targetPosition = _target.position;
            var targetLost = false;
            while (t < _flyingTime)
            {
                if (!targetLost && _target != null && _target.gameObject.activeInHierarchy)
                    targetPosition = _target.position;
                else
                    targetLost = true;

                t += Time.deltaTime;
                var progress = Mathf.Clamp01(t / _flyingTime);
                var vertical = startPosition + Vector3.up * (_animationCurve.Evaluate(progress) * _maxHeight);
                var horizontal = Vector3.Lerp(startPosition, targetPosition, progress);
                transform.position = new Vector3(horizontal.x, vertical.y, horizontal.z);
                yield return null;
            }
            DealSplashDamage();
            if (isActiveAndEnabled)
                _objectPool.Release(this);
        }

        private void DealSplashDamage()
        {
            if (_enemyDetection.TryGetEnemiesInRadius(transform.position, _splashDamageRadius, out var enemies, out var foundEnemiesCount))
            {
                for(var i = 0; i < foundEnemiesCount; i++)
                {
                    var enemy = enemies[i];
                    if (enemy != null && enemy.TryGetComponent<IDamageable>(out var damageable))
                        damageable.TakeDamage(_splashDamage);
                }
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!collider.TryGetComponent<Enemy>(out var enemy) || !enemy.isActiveAndEnabled)
                return;

            enemy.TakeDamage(_damage);
            DealSplashDamage();

            if (isActiveAndEnabled)
                _objectPool.Release(this);
        }

        public class Factory : PlaceholderFactory<Stone>
        {
        }
    }
}
