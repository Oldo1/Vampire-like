using Assets.Scripts.Entities.Projectiles;

namespace Assets.Scripts.ObjectPool
{
    public class LightningObjectPool : GameObjectPool<Lightning>
    {
        public LightningObjectPool(Lightning.Factory factory) : base(factory)
        {
        }
    }
}
