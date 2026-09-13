using Assets.Scripts.ObjectPool;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private PoolInstallerParams _enemyPoolParams;
        [SerializeField] private PoolInstallerParams _crystallPoolParams;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyObjectPool>()
                .AsSingle()
                .WithArguments(_enemyPoolParams.PreloadCount, _enemyPoolParams.Parent);

            Container.BindInterfacesAndSelfTo<CrystalObjectPool>()
                .AsSingle()
                .WithArguments(_crystallPoolParams.PreloadCount, _crystallPoolParams.Parent);

            Container.BindInterfacesAndSelfTo<FireBallObjectPool>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<LightningObjectPool>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<StoneObjectPool>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ChunkObjectPool>()
                .AsSingle();
        }

        [Serializable]
        private struct PoolInstallerParams
        {
            public int PreloadCount;
            public Transform Parent;
        }
    }
}
