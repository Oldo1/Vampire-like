using System.Collections;
using UnityEngine;
using Assets.Scripts.Configs;

namespace Assets.Scripts.Gameplay
{
    public class HitIndicator
    {
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");
        private readonly HitIndicatorConfig _config;

        public HitIndicator(HitIndicatorConfig config)
        {
            _config = config;
        }

        public void Flash(Renderer renderer, MaterialPropertyBlock block, MonoBehaviour coroutineStarter)
        {
            var flashRoutine = FlashRoutine(renderer, block);
            coroutineStarter.StartCoroutine(flashRoutine);
        }

        private IEnumerator FlashRoutine(Renderer renderer, MaterialPropertyBlock block)
        {
            SetColor(renderer, block, _config.FlashColor);
            yield return new WaitForSeconds(_config.FlashDuration);
            renderer.SetPropertyBlock(null);
        }

        private void SetColor(Renderer renderer, MaterialPropertyBlock block, Color color)
        {
            block.Clear();
            block.SetColor(BaseColorId, color);
            renderer.SetPropertyBlock(block);
        }
    }
}
