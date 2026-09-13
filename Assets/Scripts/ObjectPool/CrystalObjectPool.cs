using UnityEngine;

namespace Assets.Scripts.ObjectPool
{
    public class CrystalObjectPool : GameObjectPool<Crystal>
    {
        public CrystalObjectPool(Crystal.Factory factory, int preloadCount, Transform parent) : base(factory, preloadCount, parent)
        {
        }
    }
}
