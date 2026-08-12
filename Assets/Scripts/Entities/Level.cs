using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Level
    {
        private int _currentLevel;
        private float _currentXp;
        private float _xpToLeveUp;

        private float CurrentXp
        {
            get
            {
                return _currentXp;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException($"{nameof(CurrentXp)}");
                _currentXp = value;
            }
        }

        private readonly MonoBehaviour _coroutineStarter;

        public event Action<float> OnIncreaseLevelProgress;
        public event Action<int> OnLevelUp;

        public Level(MonoBehaviour coroutineStarter)
        {
            _coroutineStarter = coroutineStarter;
            _currentLevel = 1;
            _xpToLeveUp = 5;
        }

        public void IncreaseLevelProgress(float amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("amount can't be less or equal than 0");

            CurrentXp = Mathf.MoveTowards(CurrentXp, _xpToLeveUp, amount);
            if (CurrentXp == _xpToLeveUp)
                LevelUp();
            OnIncreaseLevelProgress?.Invoke(_currentXp / _xpToLeveUp);
        }

        private void LevelUp()
        {
            _currentLevel++;
            CurrentXp = 0;
            _xpToLeveUp += 2.5f;
            OnLevelUp?.Invoke(_currentLevel);
        }
    }
}