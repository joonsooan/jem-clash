using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image itemImage;
    public TMP_Text itemNameText;

    public void SetItem(UpgradeData item)
    {
        if (item == null)
        {
            itemImage.enabled = false;
            itemNameText.text = "";
            return;
        }

        itemImage.enabled = true;
        itemImage.sprite = item.itemImage;
        itemNameText.text = item.itemName;
    }
}