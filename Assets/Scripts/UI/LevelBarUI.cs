using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI
{
    public class LevelBarUI: MonoBehaviour
    {
        private Image _filledLevelBar;
        private Player _player;

        [Inject]
        private void Construct(Image filledLevelBar, Player player)
        {
            _filledLevelBar = filledLevelBar;
            _player = player;
        }

        private void Start()
        {
            _player.OnIncreaseLevelProgress += OnIncreaseLevelProgress;
            _player.OnLevelUp += OnLevelUp;
        }

        private void OnDestroy()
        {
            _player.OnIncreaseLevelProgress -= OnIncreaseLevelProgress;
            _player.OnLevelUp -= OnLevelUp;
        }

        private void OnIncreaseLevelProgress(float progress)
        {
            _filledLevelBar.fillAmount = progress;
        }

        private void OnLevelUp(int currentLevel)
        {
            _filledLevelBar.fillAmount = 0;
        }
    }
}
