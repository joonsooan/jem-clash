using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemDescription itemDescription;
    public Image itemImage;
    public TMP_Text itemNameText;
    private string _itemCategory;
    private string _itemCooldownTime;
    private string _itemDescription;
    private int _itemPrice;
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
            _itemCooldownTime = "";
            _itemPrice = 0;
            return;
        }

        itemImage.enabled = true;
        itemImage.sprite = item.itemImage;
        itemNameText.text = item.itemName;
        _itemDescription = item.description;
        _itemRarity = item.itemRarity.ToString();
        _itemCategory = item.itemCategory.ToString();
        _itemCooldownTime = item.cooldownTime.ToString("F1");
        _itemPrice = item.itemPrice;
    }

    public void OnClick()
    {
        itemDescription.UpdateItemDescPanel(
            itemNameText.text, _itemRarity, _itemCategory, _itemDescription, _itemCooldownTime, _itemPrice);
    }
}