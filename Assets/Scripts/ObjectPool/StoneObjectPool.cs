using Assets.Scripts.Entities.Projectiles;
using Zenject;

namespace Assets.Scripts.ObjectPool
{
    public class StoneObjectPool : GameObjectPool<Stone>
    {
        public StoneObjectPool(Stone.Factory factory) : base(factory)
        {
        }
    }
}
