using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class HitIndicator
    {
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        public void Flash(Renderer renderer, MaterialPropertyBlock block, Color color, float time, MonoBehaviour coroutineStarter)
        {
            var flashRoutine = FlashRoutine(renderer, block, color, time);
            coroutineStarter.StartCoroutine(flashRoutine);
        }

        private IEnumerator FlashRoutine(Renderer renderer, MaterialPropertyBlock block, Color color, float time)
        {
            SetColor(renderer, block, color);
            yield return new WaitForSeconds(time);
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
