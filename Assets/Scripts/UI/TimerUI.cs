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

        private int _lastElapsedSeconds = -1;

        private void Update()
        {
            int elapsedSeconds = (int)Time.timeSinceLevelLoad;

            if (elapsedSeconds == _lastElapsedSeconds)
                return;

            _lastElapsedSeconds = elapsedSeconds;

            int minutes = elapsedSeconds / 60;
            int seconds = elapsedSeconds % 60;

            _timerUI.SetText("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
