using Assets.Scripts.Gameplay;
using Assets.Scripts.Entities;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class SceneInstaller : MonoInstaller
    {
        [Header("Chunk Mover")]
        [SerializeField, Min(0)] float _raycastMaxDistance;
        [SerializeField] private LayerMask _chunkLayerMask;

        public override void InstallBindings()
        {
            Container.Bind<Camera>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<CameraFollow>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .WhenInjectedInto<CameraFollow>();

            Container.Bind<Chunk>()
                .FromComponentsInHierarchy()
                .WhenInjectedInto<ChunkMover>();

            Container.BindInterfacesAndSelfTo<ChunkMover>()
                .AsSingle()
                .WithArguments(_raycastMaxDistance, _chunkLayerMask)
                .NonLazy();
        }
    }
}