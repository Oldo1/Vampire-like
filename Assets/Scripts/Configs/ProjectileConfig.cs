using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/Projectile")]
    public class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float ProjectileLifetime { get; private set; } = 5f;
    }
}
