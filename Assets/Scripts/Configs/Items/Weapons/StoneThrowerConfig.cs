using UnityEngine;

namespace Assets.Scripts.Configs.Items.Weapons
{
    [CreateAssetMenu(fileName = "StoneThrower", menuName = "ItemInfos/Stone Thrower")]
    public class StoneThrowerConfig : ItemInfo
    {
        [field: SerializeField, Min(0)] public float Damage { get; private set; }
        [field: SerializeField, Min(0.01f)] public float Speed { get; private set; }
        [field: SerializeField, Min(0)] public float Radius { get; private set; }
        [field: SerializeField, Min(0)] public float Cooldown { get; private set; }
    }
}
