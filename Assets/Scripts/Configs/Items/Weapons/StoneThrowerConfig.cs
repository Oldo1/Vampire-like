using UnityEngine;

using Assets.Scripts.Configs.Items;

namespace Assets.Scripts.Configs.Items.Weapons
{
    [CreateAssetMenu(fileName = "StoneThrower", menuName = "ItemInfos/Stone Thrower")]
    public class StoneThrowerConfig : ShooterConfig
    {
        [field: SerializeField, Min(0)] public float Radius { get; private set; }
        [field: SerializeField, Min(0)] public float Cooldown { get; private set; }
    }
}
