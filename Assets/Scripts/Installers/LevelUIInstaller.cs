using Assets.Scripts.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class LevelUIInstaller : MonoInstaller
    {
        [SerializeField] TextMeshProUGUI _levelAmount;

        public override void InstallBindings()
        {
            Container.Bind<TextMeshProUGUI>()
                .FromInstance(_levelAmount);

            Container.Bind<LevelUI>()
                .FromComponentOnRoot();
        }
    }
}
