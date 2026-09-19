using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class SceneInstaller : MonoInstaller
    {
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
        }
    }
}