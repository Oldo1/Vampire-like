using Zenject;

namespace Assets.Scripts
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputSystem>()
                .AsCached();
        }
    }
}
