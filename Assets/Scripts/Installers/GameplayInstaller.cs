using Assets.Scripts.Configs;
using Assets.Scripts.Entities;
using Assets.Scripts.Gameplay;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("Chunk Mover")]
        [SerializeField, Min(0)] float _raycastMaxDistance;
        [SerializeField] private LayerMask _chunkLayerMask;

        [Header("Configs")]
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        [SerializeField] private DetectionConfig _crystalsDetectionConfig;
        [SerializeField] private DetectionConfig _enemyDetectionConfig;
        [SerializeField] private EnemyRotationConfig _enemyRotationConfig;
        [SerializeField] private ProjectileConfig _projectileConfig;

        [Header("ObjectsParents")]
        [SerializeField] private Transform _enemiesParent;
        [SerializeField] private Transform _crystalsParent;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemiesMover>()
                .FromSubContainerResolve()
                .ByMethod(InstallEnemyMover)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<EnemySpawner>()
                .FromSubContainerResolve()
                .ByMethod(InstallEnemySpawner)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<FireBallsMover>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .WhenInjectedInto<CrystalsMover>();

            Container.BindInterfacesAndSelfTo<CrystalsMover>()
                .AsSingle()
                .WithArguments(_crystalsDetectionConfig.DetectionRadius, _crystalsDetectionConfig.LayerMask,
                    _crystalsDetectionConfig.BufferLength)
                .NonLazy();

            Container.Bind<MovementRotator>()
                .AsSingle();

            Container.Bind<EnemyDetection>()
                .AsTransient()
                .WithArguments(_enemyDetectionConfig);

            Container.Bind<HitIndicator>()
                .AsSingle();

            Container.Bind<Transform>()
                .WithId(Enemy.CrystalsParentId)
                .FromInstance(_crystalsParent);

            Container.Bind<ProjectileDestroyer>()
                .AsSingle()
                .WithArguments(_projectileConfig.ProjectileLifetime);

            Container.Bind<Chunk>()
                .FromComponentsInHierarchy()
                .WhenInjectedInto<ChunkMover>();

            Container.BindInterfacesAndSelfTo<ChunkMover>()
                .AsSingle()
                .WithArguments(_raycastMaxDistance, _chunkLayerMask)
                .NonLazy();

            Container.Bind<Camera>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<CameraFollow>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .WhenInjectedInto<CameraFollow>();
        }

        private void InstallEnemyMover(DiContainer container)
        {
            container.Bind<float>()
                .FromInstance(_enemyRotationConfig.RotationSpeed);

            container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .AsSingle()
                .WhenInjectedInto<EnemiesMover>();

            container.BindInterfacesAndSelfTo<EnemiesMover>()
                .AsSingle();
        }

        private void InstallEnemySpawner(DiContainer container)
        {
            container.Bind<CoroutineStarter>()
                .FromNewComponentOnNewGameObject()
                .AsSingle();

            container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .AsSingle();

            container.Bind<Transform>()
                .WithId(EnemySpawner.EnemiesParentId)
                .FromInstance(_enemiesParent);

            container.Bind<EnemySpawner>()
                .AsSingle()
                .WithArguments(_enemySpawnerConfig);
        }
    }
}
