
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class FPSDisplay : MonoBehaviour
    {
        [SerializeField] private FPSCount _fpsCount;
        [SerializeField] private TextMeshProUGUI _fpsText;

        private string[] _FPS;

        private void Awake()
        {
            _FPS = Enumerable.Range(0, 1000).Select(i => $"FPS: {i}").ToArray();
        }

        private void LateUpdate()
        {
            _fpsText.text = _FPS[_fpsCount.AvarageFPS];
        }
    }
}
