using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemShopUI : MonoBehaviour
{
    private const int DISPLAY_ITEM_COUNT = 4;
    public ItemDatabase itemDatabase;
    public ItemSlot[] itemSlots;
    private List<UpgradeData> currentItems = new();

    private void Start()
    {
        RerollShop();
    }

    public void RerollShop()
    {
        currentItems = GetRandomItems(DISPLAY_ITEM_COUNT);

        for (int i = 0; i < itemSlots.Length; i++)
            if (i < currentItems.Count)
                itemSlots[i].SetItem(currentItems[i]);
            else
                itemSlots[i].SetItem(null);
    }

    private List<UpgradeData> GetRandomItems(int count)
    {
        if (itemDatabase.items.Count < count)
        {
            Debug.LogError("아이템 개수가 부족합니다!");
            return new List<UpgradeData>(itemDatabase.items); // 가능한 만큼 반환
        }

        return itemDatabase.items.OrderBy(x => Random.value).Take(count).ToList();
    }
}