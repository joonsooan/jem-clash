using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text descriptionText;
    public Image itemImage;

    private UpgradeData _itemData;

    public void SetItem(UpgradeData item)
    {
        _itemData = item;
        itemNameText.text = item.itemName;
        descriptionText.text = item.description;
        itemImage.sprite = item.itemImage;
    }
}