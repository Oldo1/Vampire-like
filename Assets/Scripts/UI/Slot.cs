using Assets.Scripts.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private Item _item;

        public Item Item 
        {
            get => _item;
            set
            {
                if (value == null)
                    throw new System.NullReferenceException("item is null");
                var icon = value.ItemInfo.Icon;
                if (icon == null)
                    throw new System.NullReferenceException("icon is null");
                _item = value;
                SetImageSprite(icon);
            }
        }

        private void SetImageSprite(Sprite sprite)
        {
            if (sprite == null)
                throw new System.NullReferenceException("sprite is null");
            _image.sprite = sprite;
        }
    }
}
