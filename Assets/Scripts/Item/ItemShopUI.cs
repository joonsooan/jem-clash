using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemShopUI : MonoBehaviour
{
    private const int DisplayItemCount = 4;
    public ItemDatabase itemDatabase;
    public ItemDescription itemDescription;
    public ItemSlot[] itemSlots;

    private List<UpgradeData> _currentItems = new();

    private void Start()
    {
        RerollShop();
    }

    public void RerollShop()
    {
        _currentItems = GetRandomItems(DisplayItemCount);

        for (int i = 0; i < itemSlots.Length; i++)
            if (i < _currentItems.Count)
                itemSlots[i].SetItem(_currentItems[i]);
            else
                itemSlots[i].SetItem(null);

        itemDescription.EraseDescPanel();
    }

    private List<UpgradeData> GetRandomItems(int count)
    {
        if (itemDatabase.items.Count < count)
        {
            Debug.LogError("아이템 개수가 부족합니다!");
            return new List<UpgradeData>(itemDatabase.items);
        }

        var items = new List<UpgradeData>(itemDatabase.items);
        int n = items.Count;

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(i, n);
            (items[i], items[randomIndex]) = (items[randomIndex], items[i]);
        }

        return items.Take(count).ToList();
    }
}