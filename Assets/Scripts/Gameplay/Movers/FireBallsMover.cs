using Assets.Scripts.Entities.Projectiles;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using System.Collections.Generic;
using Zenject;

namespace Assets.Scripts
{

    public class FireBallsMover : ITickable, IInitializable, System.IDisposable
    {
        public bool Enabled { get; set; }

        private readonly FireBallObjectPool _fireBallObjectPool;
        private readonly HashSet<FireBall> _activeFireBalls;


        public FireBallsMover(FireBallObjectPool projectileObjectPool)
        {
            _fireBallObjectPool = projectileObjectPool;
            Enabled = true;
            _activeFireBalls = new HashSet<FireBall>();
        }

        public void Initialize()
        {
            _fireBallObjectPool.OnGet += OnPoolGet;
            _fireBallObjectPool.OnRelease += OnPoolRelease;
        }

        private void OnPoolGet(FireBall fireBall)
        {
            _activeFireBalls.Add(fireBall);
        }

        private void OnPoolRelease(FireBall fireBall)
        {
            _activeFireBalls.Remove(fireBall);
        }

        public void Tick()
        {
            if (!Enabled) return;

            foreach (var projectile in _activeFireBalls)
                if (projectile is IMovable movable)
                    movable.Move();
        }

        public void Dispose()
        {
            _fireBallObjectPool.OnGet -= OnPoolGet;
            _fireBallObjectPool.OnRelease -= OnPoolRelease;
        }
    }
}

