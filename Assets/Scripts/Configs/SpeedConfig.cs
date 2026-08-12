using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu]
    public class SpeedConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float Value { get; private set; }
    }
}
