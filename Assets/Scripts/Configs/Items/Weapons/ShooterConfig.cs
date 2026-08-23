using UnityEngine;

namespace Assets.Scripts.Configs.Items
{
    public abstract class ShooterConfig : ItemInfo
    {
        [field: SerializeField, Min(0)] public float Damage { get; private set; }
        [field: SerializeField, Min(0)] public float Speed { get; private set; }
        [field: SerializeField, Min(1)] public int ProjectileAmount { get; private set; } = 1;
    }
}
