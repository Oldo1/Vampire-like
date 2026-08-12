using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class LevelUI : MonoBehaviour
    {
        private TextMeshProUGUI _levelAmount;
        private Player _player;

        [Inject]
        private void Construct(TextMeshProUGUI levelAmount, Player player)
        {
            _player = player;
            _levelAmount = levelAmount;
        }

        private void Start()
        {
            _player.OnLevelUp += OnLevelUp;
        }

        private void OnDestroy()
        {
            _player.OnLevelUp -= OnLevelUp;
        }

        private void OnLevelUp(int currentLevel)
        {
            _levelAmount.text = currentLevel.ToString();
        }
    }
}
