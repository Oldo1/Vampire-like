using UnityEngine;

namespace Assets.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "SpeedBoosterConfig", menuName = "ItemInfos/Speed Booster")]
    public class SpeedBoosterConfig : ItemInfo
    {
        [field: SerializeField, Min(0)] public float SpeedMultipllierAmount { get; private set; }
    }
}
