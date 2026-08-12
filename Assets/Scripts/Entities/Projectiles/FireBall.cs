using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace Assets.Scripts.Entities.Projectiles
{
    public class FireBall : Projectile, IMovable, IAttacker
    {
        public Vector3 Direction { get; private set; }

        private float _damage;
        private float _speed;
        private ProjectileDestroyer _projectileDestroyer;
        private FireBallObjectPool _fireBallObjectPool;

        [Inject]
        private void Construct(ProjectileDestroyer projectileDestroyer, FireBallObjectPool fireBallObjectPool)
        {
            _projectileDestroyer = projectileDestroyer;
            _fireBallObjectPool = fireBallObjectPool;
        }

        public override void Launch(Transform target, float damage, float speed)
        {
            if (target == null)
                throw new System.ArgumentNullException(nameof(target));
            if (damage < 0)
                throw new System.ArgumentOutOfRangeException(nameof(damage));
            if (speed <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(speed));

            _damage = damage;
            _speed = speed;
            Direction = (target.position - transform.position).normalized;

            if (Direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(Direction);

            var routine = _projectileDestroyer.DestroyProjectileRoutine(_fireBallObjectPool, this);
            StartCoroutine(routine);
        }

        public void Move()
        {
            transform.position += Direction * (_speed * Time.deltaTime);
        }

        public void Attack(IDamageable target)
        {
            target.TakeDamage(_damage);
        }

        public class Factory : PlaceholderFactory<FireBall>
        {
        }
    }
}
