using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu]
    public class CooldownConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float Value { get; private set; }
    }
}
