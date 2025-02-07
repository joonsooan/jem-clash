using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemDescription itemDescription;
    public Image itemImage;
    public TMP_Text itemNameText;
    private string _itemCategory;
    private string _itemDescription;
    private string _itemRarity;

    public void SetItem(UpgradeData item)
    {
        if (item == null)
        {
            itemImage.enabled = false;
            itemNameText.text = "";
            _itemDescription = "";
            _itemRarity = "";
            _itemCategory = "";
            return;
        }

        itemImage.enabled = true;
        itemImage.sprite = item.itemImage;
        itemNameText.text = item.itemName;
        _itemDescription = item.description;
        _itemRarity = item.itemRarity.ToString();
        _itemCategory = item.itemCategory.ToString();
    }

    public void OnClick()
    {
        itemDescription.UpdateItemDescPanel();
    }
}