using UnityEngine;

namespace Assets.Scripts.Configs.Items
{
    public class ItemInfo : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField ] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField, Min(0)] public int MaxLevel { get; private set; }
    }
}
