using UnityEngine;

namespace Assets.Scripts.Configs.Items.Weapons
{
    [CreateAssetMenu(fileName = "DamageAura", menuName = "ItemInfos/Damage Aura")]
    public class DamageAuraConfig : ItemInfo
    {
        [field: SerializeField, Min(0)] public float Damage { get; private set; }
        [field: SerializeField, Min(0)] public float Radius { get; private set; }
        [field: SerializeField, Min(0)] public float Cooldown { get; private set; }
    }
}
