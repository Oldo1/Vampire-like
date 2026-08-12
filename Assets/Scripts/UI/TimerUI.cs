using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class TimerUI: MonoBehaviour
    {
        public TextMeshProUGUI _timerUI;

        private float _seconds;

        private void Start()
        {
            _timerUI.text = "00:00";
        }

        private void Update()
        {
            var seconds = (int)(Time.timeSinceLevelLoad % 60);
            var minutes = (int)(Time.timeSinceLevelLoad / 60);
            _timerUI.text = $"{minutes:D2}:{seconds:D2}";
        }
    }
}
