using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class HitIndicator
    {
        public void Flash(Material material, Color color, Color baseColor, float time, MonoBehaviour coroutineStarter)
        {
            var flashRoutine = FlashRoutine(material, color, baseColor, time);
            coroutineStarter.StartCoroutine(flashRoutine);
        }

        private IEnumerator FlashRoutine(Material material, Color color, Color baseColor, float time)
        {
            material.color = color;
            yield return new WaitForSeconds(time);
            material.color = baseColor;
        }
    }
}
