using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu]
    public class EnemySpawnerConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float SpawnRadius { get; private set; }
        [field: SerializeField, Min(0)] public float SpawnRate { get; private set; }
    }
}
