using TMPro;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    [Header("Default Text")] public TMP_Text rarityDefaultText;
    public TMP_Text categoryDefaultText;
    public TMP_Text coolTimeDefaultText;

    [Header("Item Description")] public TMP_Text nameText;
    public TMP_Text rarityText;
    public TMP_Text categoryText;
    public TMP_Text descriptionText;
    public TMP_Text coolTimeText;

    private void Start()
    {
        EraseDescPanel();
    }

    public void UpdateItemDescPanel(string itemName, string rarity, string category, string description,
        string coolTime)
    {
        InitDescPanel();

        nameText.text = itemName;
        rarityText.text = rarity;
        categoryText.text = category;
        descriptionText.text = description;
        coolTimeText.text = $"{coolTime} s";
    }

    private void InitDescPanel()
    {
        rarityDefaultText.text = "Rarity";
        categoryDefaultText.text = "Type";
        coolTimeDefaultText.text = "Cooldown";
    }

    public void EraseDescPanel()
    {
        rarityDefaultText.text = "";
        categoryDefaultText.text = "";
        coolTimeDefaultText.text = "";
        nameText.text = "";
        rarityText.text = "";
        categoryText.text = "";
        descriptionText.text = "";
        coolTimeText.text = "";
    }
}