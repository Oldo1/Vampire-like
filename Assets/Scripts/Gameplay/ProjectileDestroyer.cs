using Assets.Scripts.ObjectPool;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ProjectileDestroyer
    {
        private readonly float _lifeTime;

        public ProjectileDestroyer(float lifeTime)
        {
            if (lifeTime < 0)
                throw new System.ArgumentOutOfRangeException();

            _lifeTime = lifeTime;
        }

        public IEnumerator DestroyProjectileRoutine<T>(GameObjectPool<T> gameObjectPool, T projectile) where T : Projectile
        {
            yield return new WaitForSeconds(_lifeTime);

            if (projectile != null && projectile.isActiveAndEnabled)
                gameObjectPool.Release(projectile);
        }
    }
}
