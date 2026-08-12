using Assets.Scripts;
using Assets.Scripts.Configs.Items;
using Assets.Scripts.Items;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeCard : MonoBehaviour, IInitializable, IDisposable
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _levelAmount;
    [SerializeField] private GameObject _newLabel;
    [SerializeField] private GameObject _level;
    [SerializeField] private CardSelectHandler _selectHandler;
    [SerializeField] private Image _image;

    public event Action<UpgradeCard> OnCardSelect 
    { 
        add { _selectHandler.OnCardSelect += value; }
        remove { _selectHandler.OnCardSelect -= value; } 
    }

    public Item Item { get; private set; }
    public ItemInfo ItemInfo => Item.ItemInfo;

    [Inject]
    private void Construct(Item item)
    {
        Item = item;
    }

    public void Initialize()
    {
        _name.text = ItemInfo.Name;
        _description.text = ItemInfo.Description;
        _image.sprite = ItemInfo.Icon;

        Item.OnLevelUp += OnLevelUp;
    }

    public void OnLevelUp(int currentLevel)
    {
        if (currentLevel == 1)
        {
            _newLabel.SetActive(false);
            _level.SetActive(true);
        }

        _levelAmount.text = currentLevel.ToString();
    }

    public void Dispose()
    {
        Item.OnLevelUp -= OnLevelUp;
    }
}
