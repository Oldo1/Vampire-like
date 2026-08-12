using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class LevelBarUIInstaller : MonoInstaller
    {
        [SerializeField] private Image _filledLevelBar;

        public override void InstallBindings()
        {
            Container.Bind<Image>()
                .FromInstance(_filledLevelBar);

            Container.Bind<LevelBarUI>()
                .FromComponentOnRoot()
                .AsSingle();
        }
    }
}
