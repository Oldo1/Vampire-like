using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Entities.Projectiles
{
    public class DamageAura : MonoBehaviour
    {
        [SerializeField] private Transform _visual;

        private EnemyDetection _enemyDetection;
        private const float CIRCLE_DIAMETER = 1.66f;

        private float _damage;
        private float _cooldown;
        private float _radius;

        [Inject]
        private void Construct(EnemyDetection enemyDetection)
        {
            _enemyDetection = enemyDetection;
        }

        public void StartDealDamage(float cooldown, float radius, float damage)
        {
            _cooldown = cooldown;
            _radius = radius;
            _damage = damage;

            if (_visual != null)
                _visual.localScale = Vector3.one * (_radius / CIRCLE_DIAMETER);

            StartCoroutine(DamageRoutine());
        }

        private IEnumerator DamageRoutine()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            var waitForCooldown = new WaitForSeconds(_cooldown);

            while (true)
            {
                var position = transform.position;
                if (_enemyDetection.TryGetEnemiesInRadius(position, _radius, out var enemies, out var foundEnemiesCount))
                {
                    for (var i = 0; i < foundEnemiesCount; i++)
                    {
                        var enemy = enemies[i];
                        if (enemy.TryGetComponent<IDamageable>(out var damageable))
                            damageable.TakeDamage(_damage);
                    }
                }
                yield return waitForCooldown;
                yield return waitForFixedUpdate;
            }
        }

        public class Factory : PlaceholderFactory<DamageAura>
        {
        }
    }
}
