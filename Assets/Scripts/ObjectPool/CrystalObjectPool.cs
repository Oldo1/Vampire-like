using Zenject;

namespace Assets.Scripts.ObjectPool
{
    public class CrystalObjectPool : GameObjectPool<Crystal>
    {
        public CrystalObjectPool(Crystal.Factory factory) : base(factory)
        {
        }
    }
}
