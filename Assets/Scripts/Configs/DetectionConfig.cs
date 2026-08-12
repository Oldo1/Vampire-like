using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu]
    public class DetectionConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public int BufferLength { get; private set; }
        [field: SerializeField, Min(0)] public float DetectionRadius { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
    }
}
