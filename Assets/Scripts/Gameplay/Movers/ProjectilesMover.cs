using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using Zenject;

namespace Assets.Scripts
{

    public class ProjectilesMover : ITickable
    {
        public bool Enabled { get; set; }

        private readonly FireBallObjectPool _projectileObjectPool;

        public ProjectilesMover(FireBallObjectPool projectileObjectPool)
        {
            _projectileObjectPool = projectileObjectPool;
            Enabled = true;
        }

        public void Tick()
        {
            if (!Enabled) return;

            foreach (var projectile in _projectileObjectPool.ActiveObjects)
                if (projectile is IMovable)
                    (projectile as IMovable).Move();
        }
    }
}

