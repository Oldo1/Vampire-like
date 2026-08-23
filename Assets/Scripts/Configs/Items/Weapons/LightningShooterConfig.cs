using UnityEngine;

using Assets.Scripts.Configs.Items;

namespace Assets.Scripts.Configs.Items.Weapons
{
    [CreateAssetMenu(fileName = "LightningShooter", menuName = "ItemInfos/Lightning Shooter")]
    public class LightningShooterConfig : ShooterConfig
    {
        [field: SerializeField, Min(0)] public float Cooldown { get; private set; }
        [field: SerializeField, Min(0)] public float Radius { get; private set; }
    }
}
