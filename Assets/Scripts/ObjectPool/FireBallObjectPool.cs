using Assets.Scripts.Entities.Projectiles;

namespace Assets.Scripts.ObjectPool
{
    public class FireBallObjectPool : GameObjectPool<FireBall>
    {
        public FireBallObjectPool(FireBall.Factory factory) : base(factory)
        {
        }
    }
}
