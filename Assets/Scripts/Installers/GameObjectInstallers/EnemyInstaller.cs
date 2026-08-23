using Assets.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField, Min(0)] private float _speed;
        [SerializeField, Min(0)] private float _maxHealth;
        [SerializeField] private GameObject _healthBar;
        [SerializeField] private Image _filledHealthBar;
        [SerializeField] private Transform _model;

        public override void InstallBindings()
        {
            Container.Bind<Renderer>()
                .FromComponentsInChildren()
                .AsSingle();

            Container.Bind<CharacterController>()
                .FromComponentOnRoot()
                .AsSingle();

            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_maxHealth);

            Container.Bind<float>()
                .FromInstance(_speed)
                .WhenInjectedInto<Enemy>();

            Container.BindInterfacesAndSelfTo<Enemy>()
                .FromComponentOnRoot()
                .AsSingle();

            Container.Bind<GameObject>()
                .FromInstance(_healthBar)
                .WhenInjectedInto<BillBoard>();

            Container.Bind<BillBoard>()
                .FromComponentOnRoot()
                .AsSingle();

            Container.Bind<Image>()
                .FromInstance(_filledHealthBar)
                .AsSingle()
                .WhenInjectedInto<OnEnemyTakeDamage>();

            Container.BindInstance(_model)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<OnEnemyTakeDamage>()
                .AsSingle()
                .NonLazy();
        }
    }
}
