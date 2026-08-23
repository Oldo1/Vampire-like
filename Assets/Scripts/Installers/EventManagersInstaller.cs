using Assets.Scripts.Gameplay;
using Zenject;

namespace Assets.Scripts
{
    public class EventManagersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<OnCardAddInInventory>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<OnCardSelectManager>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<OnSlotFilledManager>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<OnLevelUpManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}
