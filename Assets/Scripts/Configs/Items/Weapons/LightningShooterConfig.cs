using UnityEngine;

namespace Assets.Scripts.Configs.Items.Weapons
{
    [CreateAssetMenu(fileName = "LightningShooter", menuName = "ItemInfos/Lightning Shooter")]
    public class LightningShooterConfig : ItemInfo
    {
        [field: SerializeField, Min(0)] public float Damage { get; private set; }
        [field: SerializeField, Min(0)] public float Speed { get; private set; }
        [field: SerializeField, Min(0)] public float Cooldown { get; private set; }
        [field: SerializeField, Min(0)] public float Radius { get; private set; }
    }
}
