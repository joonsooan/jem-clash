using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Buy Values")] public ItemManager itemManager;
    public Button buyButton;
    public TMP_Text itemPriceText;
    public int itemPrice;

    private UpgradeData _selectedItem;

    private void Start()
    {
        EraseDescPanel();
        buyButton.onClick.AddListener(() => BuyItem(itemPrice));
        itemPriceText.text = itemPrice.ToString();
    }

    public void UpdateItemDescPanel(string itemName, string rarity, string category, string description,
        string coolTime, int price)
    {
        InitDescPanel();

        nameText.text = itemName;
        rarityText.text = rarity;
        categoryText.text = category;
        descriptionText.text = description;
        coolTimeText.text = $"{coolTime} s";

        SetItemPrice(price);
    }

    private void InitDescPanel()
    {
        rarityDefaultText.text = "Rarity";
        categoryDefaultText.text = "Type";
        coolTimeDefaultText.text = "Cooldown";
    }

    private void SetItemPrice(int newPrice)
    {
        itemPrice = newPrice;
        itemPriceText.text = itemPrice.ToString();
    }

    public void SetSelectedItem(UpgradeData item)
    {
        _selectedItem = item;
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

        _selectedItem = null;
    }

    private void BuyItem(int amount)
    {
        if (MoneyManager.Instance.SubtractMoney(amount))
            itemManager.CreateItemBtn(_selectedItem);
    }
}