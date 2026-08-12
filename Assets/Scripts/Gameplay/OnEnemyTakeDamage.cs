using System;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class OnEnemyTakeDamage : IInitializable, IDisposable
    {
        private readonly Enemy _enemy;
        private readonly Image _filledHealthBar;

        public OnEnemyTakeDamage(Enemy enemy, Image filledHealthBar)
        {
            _enemy = enemy;
            _filledHealthBar = filledHealthBar;
        }

        public void Initialize()
        {
            _enemy.OnTakeDamage += OnTakeDamage;
        }

        private void OnTakeDamage(float progress)
        {
            _filledHealthBar.fillAmount = progress;
        }

        public void Dispose()
        {
            _enemy.OnTakeDamage -= OnTakeDamage;
        }
    }
}
