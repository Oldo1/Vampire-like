using UnityEngine;

namespace Assets.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "RepeaterConfig", menuName = "ItemInfos/Repeater")]
    public class RepeaterConfig : ItemInfo
    {
        [field: SerializeField] public float ProjectileAmount { get; private set; }
    }
}
