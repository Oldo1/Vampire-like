using UnityEngine;

namespace Assets.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "FireBallShooterConfig", menuName = "ItemInfos/Fire Ball Shooter")]
    public class FireBallShooterConfig : ShooterConfig
    {
        [field: SerializeField, Min(0)] public float DamageMultiplierAmount { get; private set; }
        [field: SerializeField, Min(0)] public float Cooldown { get; private set; }
        [field: SerializeField, Min(0)] public float Radius { get; private set; }

    }
}
