using UnityEngine;

namespace Assets.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "CooldownReducerConfig", menuName = "ItemInfos/Cooldown Reducer")]
    public class CooldownReducerConfig : ItemInfo
    {
        [field: SerializeField, Min(0)] public float СooldownReduceMultiplier { get; private set; }
    }
}
