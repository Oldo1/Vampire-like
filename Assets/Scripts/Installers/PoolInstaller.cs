using Assets.Scripts.ObjectPool;
using Zenject;

namespace Assets.Scripts
{
    public class PoolInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyObjectPool>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<CrystalObjectPool>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<FireBallObjectPool>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<LightningObjectPool>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<StoneObjectPool>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ChunkObjectPool>()
                .AsSingle();
        }
    }
}
