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

    [Header("Buy Values")] public Button buyButton;
    public TMP_Text itemPriceText;
    public int itemPrice;

    private void Start()
    {
        EraseDescPanel();
        buyButton.onClick.AddListener(() => BuyItem(itemPrice));
        itemPriceText.text = itemPrice.ToString();
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

    // TODO: 아이템을 선택한 상태여야지만 아이템을 구매할 수 있음
    private void BuyItem(int amount)
    {
        MoneyManager.Instance.SubtractMoney(amount);
    }
}