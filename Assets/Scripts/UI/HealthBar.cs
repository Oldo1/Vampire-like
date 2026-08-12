using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _filledHealthBar;

        private void OnEnable()
        {
            _filledHealthBar.fillAmount = 1;
        }
    }
}
