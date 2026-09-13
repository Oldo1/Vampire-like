using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class FPSCount : MonoBehaviour
    {
        private int _frameRange = 80;

        private int[] _fpsBuffer;
        private int _fpsBufferIndex;

        public int AvarageFPS { get; private set; }

        private void Awake()
        {
            IntializeBuffer();
        }

        private void Update()
        {
            UpdateBuffer();
            CalculateAverageFPS();
        }

        private void UpdateBuffer()
        {
            _fpsBuffer[_fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
            if (_fpsBufferIndex >= _frameRange)
                _fpsBufferIndex = 0;
        }

        private void CalculateAverageFPS()
        {
            AvarageFPS = _fpsBuffer.Sum() / _fpsBuffer.Length;
        }

        private void IntializeBuffer()
        {
            if (_frameRange <= 60)
                _frameRange = 1;

            _fpsBuffer = new int[_frameRange];
            _fpsBufferIndex = 0;
        }
    }
}
