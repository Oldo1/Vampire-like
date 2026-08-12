using Assets.Scripts.Entities.Projectiles;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class ProjectileOnHitController : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;

        private FireBallObjectPool _projectileObjectPool;
        private bool _hasHit;

        [Inject]
        private void Construct(FireBallObjectPool projectileObjectPool)
        {
            _projectileObjectPool = projectileObjectPool;
        }

        private void OnEnable()
        {
            _hasHit = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_hasHit)
                return;

            if (other.TryGetComponent<Enemy>(out var target))
            {
                _hasHit = true;

                if (_projectile is IAttacker attacker)
                    attacker.Attack(target);

                if (isActiveAndEnabled)
                    _projectileObjectPool.Release(GetComponent<FireBall>());
            }
        }
    }
}



