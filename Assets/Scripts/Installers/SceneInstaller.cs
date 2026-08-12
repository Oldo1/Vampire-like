using Assets.Scripts.Configs;
using Assets.Scripts.Configs.Items;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Projectiles;
using Assets.Scripts.Gameplay;
using Assets.Scripts.GameStates;
using Assets.Scripts.ObjectPool;
using Assets.Scripts.PlayerScripts;
using Assets.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class SceneInstaller : MonoInstaller
    {
        [Header("Preafabs")]
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Crystal _crystalPrefab;
        [SerializeField] private FireBall _fireBallPrefab;
        [SerializeField] private Lightning _lightningPrefab;
        [SerializeField] private Stone _stonePrefab;
        [SerializeField] private DamageAura _damageAuraPrefab;
        [SerializeField] private GameObject _chunkPrefab;
        [SerializeField] private Transform _groundTransform;

        [Header("Configs")]
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        [SerializeField] private DetectionConfig _crystalsDetectionConfig;
        [SerializeField] private DetectionConfig _enemyDetectionConfig;
        [SerializeField] private FireBallShooterConfig _fireBallShooterInfo;
        [SerializeField, Min(0)] private float _enemyRotationSpeed;

        [Header("Chunk Spawner")]
        [SerializeField, Min(0)] float _raycastMaxDistance;
        [SerializeField] private LayerMask _chunkLayerMask;

        [Header("UI")]
        [SerializeField] private GameObject _panelUI;

        [Header("Coroutine Starter")]
        [SerializeField] private CoroutineStarter _coroutineStarter;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<OnCardAddInInventory>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UpgradeCardsContainer>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UpgradeCard>()
                .FromComponentsInHierarchy()
                .AsCached();

            Container.Bind<Camera>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<MovementRotator>()
                .AsSingle();

            Container.Bind<ItemInfo>()
                .FromInstance(_fireBallShooterInfo)
                .WhenInjectedInto<Player>();

            Container.BindInterfacesAndSelfTo<Player>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(_playerPrefab)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<EnemyMover>()
                .FromSubContainerResolve()
                .ByMethod(InstallEnemyMover)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<EnemySpawner>()
                .FromSubContainerResolve()
                .ByMethod(InstallEnemySpawner)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<ProjectilesMover>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .WhenInjectedInto<CrystalsMover>();

            Container.BindInterfacesAndSelfTo<CrystalsMover>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .WhenInjectedInto<CameraFollow>();

            Container.Bind<CameraFollow>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<ProjectileDestroyer>()
                .AsSingle()
                .WithArguments(5f);

            Container.Bind<Chunk>()
                .FromComponentInHierarchy(true)
                .WhenInjectedInto<ChunkSpawner>();

            Container.Bind<ChunkObjectPool>()
                .AsSingle();

            Container.BindFactory<Chunk, Chunk.Factory>()
                .FromComponentInNewPrefab(_chunkPrefab)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ChunkSpawner>()
                .AsSingle()
                .WithArguments(_groundTransform, _raycastMaxDistance, _chunkLayerMask)
                .NonLazy();

            Container.Bind<CrystalObjectPool>()
                .AsSingle();

            Container.BindFactory<Crystal, Crystal.Factory>()
                .FromComponentInNewPrefab(_crystalPrefab)
                .AsSingle();

            Container.Bind<FireBallObjectPool>()
                .AsSingle();

            Container.BindFactory<FireBall, FireBall.Factory>()
                .FromComponentInNewPrefab(_fireBallPrefab)
                .AsSingle();

            Container.BindFactory<Lightning, Lightning.Factory>()
                .FromComponentInNewPrefab(_lightningPrefab)
                .AsSingle();

            Container.BindFactory<Stone, Stone.Factory>()
                .FromComponentInNewPrefab(_stonePrefab)
                .AsSingle();

            Container.BindFactory<Enemy, Enemy.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(_enemyPrefab)
                .AsSingle();

            Container.BindFactory<DamageAura, DamageAura.Factory>()
                .FromComponentInNewPrefab(_damageAuraPrefab)
                .AsSingle();

            Container.Bind<EnemyObjectPool>()
                .AsSingle();

            Container.Bind<LightningObjectPool>()
                .AsSingle();

            Container.Bind<StoneObjectPool>()
                .AsSingle();

            Container.Bind<LevelBarUI>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<LevelUI>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UpgradeMenu>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<BoosterSlots>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<WeaponSlots>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<Inventory>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<GameStateMachine>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UpgradeSelectionState>()
                .AsSingle()
                .WithArguments(_panelUI);

            Container.BindInterfacesAndSelfTo<UpgradeManager>()
                .AsSingle()
                .NonLazy();

            Container.Bind<CrystalsDetection>()
                .FromSubContainerResolve()
                .ByMethod(InstallCrystalsDetection)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<OnLevelUpManager>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Joystick>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<InputSystem>()
                .AsCached();

            Container.Bind<MonoBehaviour>()
                .FromResolveGetter<Player>(x => x)
                .AsSingle();

            Container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .AsSingle()
                .WhenInjectedInto<EnemyDetection>();

            Container.Bind<EnemyDetection>()
                .AsTransient()
                .WithArguments(_enemyDetectionConfig);
        }

        private void InstallEnemyMover(DiContainer container)
        {
            container.Bind<float>()
                .WithId(EnemyMover.RotationSpeedId)
                .FromInstance(_enemyRotationSpeed);

            container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .AsSingle()
                .WhenInjectedInto<EnemyMover>();

            container.BindInterfacesAndSelfTo<EnemyMover>()
                .AsSingle();
        }

        private void InstallCrystalsDetection(DiContainer container)
        {
            container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .AsSingle();

            container.Bind<CrystalsDetection>()
                .AsSingle()
                .WithArguments(_crystalsDetectionConfig.DetectionRadius, _crystalsDetectionConfig.LayerMask, _crystalsDetectionConfig.BufferLength);
        }

        private void InstallEnemySpawner(DiContainer container)
        {
            container.Bind<Transform>()
                .FromResolveGetter<Player>(x => x.transform)
                .AsSingle();

            container.Bind<EnemySpawner>()
                .AsSingle()
                .WithArguments(_enemySpawnerConfig, _coroutineStarter);
        }
    }
}




