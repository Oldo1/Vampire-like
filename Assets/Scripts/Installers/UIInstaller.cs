using Assets.Scripts.Gameplay;
using Assets.Scripts.GameStates;
using Assets.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private PanelUI _panel;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UpgradeCard>()
                .FromComponentsInHierarchy(includeInactive: true)
                .AsCached();

            Container.BindInterfacesAndSelfTo<UpgradeCardsContainer>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UpgradeMenu>()
                .FromComponentInHierarchy(true)
                .AsSingle();

            Container.Bind<Inventory>()
                .FromComponentInHierarchy(true)
                .AsSingle();

            Container.Bind<BoosterSlots>()
                .FromComponentInHierarchy(true)
                .AsSingle();

            Container.Bind<WeaponSlots>()
                .FromComponentInHierarchy(true)
                .AsSingle();

            Container.Bind<Joystick>()
                .FromComponentInHierarchy(true)
                .AsSingle();

            Container.Bind<PanelUI>()
                .WithId(UpgradeSelectionState.PanelId)
                .FromInstance(_panel)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UpgradeSelectionState>()
                .AsSingle();

            Container.Bind<GameStateMachine>()
                .AsSingle();

            Container.Bind<LevelBarUI>()
                .FromComponentInHierarchy(true)
                .AsSingle();

            Container.Bind<LevelUI>()
                .FromComponentInHierarchy(true)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerInventoryInitializer>()
                .AsSingle();
        }
    }
}
