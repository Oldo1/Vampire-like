using UnityEngine;

namespace Assets.Scripts.ObjectPool
{
    public class EnemyObjectPool : GameObjectPool<Enemy>
    {
        public EnemyObjectPool(Enemy.Factory factory, int preloadCount, Transform parent) : base(factory, preloadCount, parent)
        {
        }
    }
}
