namespace Assets.Scripts.ObjectPool
{
    public class EnemyObjectPool : GameObjectPool<Enemy>
    {
        public EnemyObjectPool(Enemy.Factory factory) : base(factory)
        {
        }
    }
}
