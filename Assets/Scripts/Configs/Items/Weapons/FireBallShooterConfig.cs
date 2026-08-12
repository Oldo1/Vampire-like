using UnityEngine;

namespace Assets.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "FireBallShooterConfig", menuName = "ItemInfos/Fire Ball Shooter")]
    public class FireBallShooterConfig : ItemInfo
    {
        [field: SerializeField, Min(0)] public float Damage { get; private set; }
        [field: SerializeField, Min(0.01f)] public float Speed { get; private set; }
        [field: SerializeField, Min(0)] public float ProjectileAmount { get; private set; }
        [field: SerializeField, Min(0)] public float DamageMultiplierAmount { get; private set; }
        [field: SerializeField, Min(0)] public float Cooldown { get; private set; }
        [field: SerializeField, Min(0)] public float Radius { get; private set; }

    }
}
