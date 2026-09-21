using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemyRotationConfig", menuName = "Configs/Enemy Rotation")]
    public class EnemyRotationConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = 720f;
    }
}
