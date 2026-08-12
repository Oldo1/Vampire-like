using Assets.Scripts.Entities;

namespace Assets.Scripts.ObjectPool
{
    public class ChunkObjectPool : GameObjectPool<Chunk>
    {
        public ChunkObjectPool(Chunk.Factory factory) : base(factory)
        {
        }
    }
}
