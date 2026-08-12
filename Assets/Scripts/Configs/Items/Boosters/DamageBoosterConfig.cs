using UnityEngine;

namespace Assets.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "DamageBoosterConfig", menuName = "ItemInfos/Damage Booster")]
    public class DamageBoosterConfig : ItemInfo
    {
        [field: SerializeField] public float DamageMultiplierAmount { get; private set; } 
    }
}
