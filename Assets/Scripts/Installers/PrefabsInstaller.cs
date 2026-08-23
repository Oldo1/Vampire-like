using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Projectiles;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class PrefabsInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Crystal _crystalPrefab;
        [SerializeField] private FireBall _fireBallPrefab;
        [SerializeField] private Lightning _lightningPrefab;
        [SerializeField] private Stone _stonePrefab;
        [SerializeField] private DamageAura _damageAuraPrefab;
        [SerializeField] private GameObject _chunkPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Player>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(_playerPrefab)
                .AsSingle()
                .NonLazy();

            Container.BindFactory<Enemy, Enemy.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(_enemyPrefab)
                .AsSingle();

            Container.BindFactory<Crystal, Crystal.Factory>()
                .FromComponentInNewPrefab(_crystalPrefab)
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

            Container.BindFactory<DamageAura, DamageAura.Factory>()
                .FromComponentInNewPrefab(_damageAuraPrefab)
                .AsSingle();

            Container.BindFactory<Chunk, Chunk.Factory>()
                .FromComponentInNewPrefab(_chunkPrefab)
                .AsSingle();
        }
    }
}
