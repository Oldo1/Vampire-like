using System.Collections.Generic;

namespace Assets.Scripts.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            for (var i = list.Count - 1; i > 0; i--)
            {
                var randomIndex = UnityEngine.Random.Range(0, i + 1);
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
            }
        }
    }
}
