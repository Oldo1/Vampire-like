using Assets.Scripts.Configs;
using Assets.Scripts.PlayerScripts;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private SpeedConfig _playerSpeed;
        [SerializeField, Min(0)] private float _rotationSpeed;
        [SerializeField] private DetectionConfig _enemyDetectionConfig;
        [SerializeField] private CooldownConfig _cooldownConfig; 

        public override void InstallBindings()
        {
            Container.BindInstance(_playerSpeed.Value)
                .AsSingle()
                .WhenInjectedInto<PlayerMover>();

            Container.Bind<float>()
                .WithId(PlayerMover.RotationSpeedId)
                .FromInstance(_rotationSpeed);

            Container.Bind<PlayerMover>()
                .FromComponentOnRoot()
                .AsSingle();

            Container.Bind<PlayerInput>()
                .AsSingle();

            Container.Bind<CharacterController>()
                .FromComponentOnRoot()
                .AsSingle();

            Container.BindInstance(_playerSpeed)
                .AsSingle();

            Container.Bind<CrystalCollector>()
                .FromComponentOnRoot()
                .AsSingle();

            Container.Bind<MonoBehaviour>()
                .FromResolveGetter<Player>(x => x)
                .AsSingle()
                .WhenInjectedInto<Level>();

            Container.Bind<Level>()
                .AsSingle();

            Container.Bind<Player>()
                .FromComponentOnRoot()
                .AsSingle();
        }
    }
}
