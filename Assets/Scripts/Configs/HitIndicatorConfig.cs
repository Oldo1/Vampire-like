using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "HitIndicatorConfig", menuName = "Configs/Hit Indicator")]
    public class HitIndicatorConfig : ScriptableObject
    {
        [field: SerializeField] public Color FlashColor { get; private set; } = Color.red;
        [field: SerializeField, Min(0)] public float FlashDuration { get; private set; } = 0.15f;
    }
}
