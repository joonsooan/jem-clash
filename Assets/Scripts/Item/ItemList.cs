using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public GameObject itemBtnPrefab;

    private void Start()
    {
        // CreateItemBtns();
    }

    private void CreateItemBtns()
    {
        // 이놈이 메모리 폭주 범인
        // 근데 왜 폭주하는거지?
        var items = ItemManager.instance.items;

        for (int i = 0; i < items.Count; i++) CreateItemBtn(items[i]);
    }

    public void CreateItemBtn(UpgradeData data)
    {
        if (data == null) return;

        GameObject buttonObj = Instantiate(itemBtnPrefab, transform);
        Upgrade upgrade = buttonObj.GetComponent<Upgrade>();
        ItemManager.instance.AddItem(data);

        if (upgrade != null)
        {
            upgrade.upgradeData = data;
            buttonObj.GetComponent<Image>().sprite = upgrade.upgradeData.itemImage;
            upgrade.level = 0;
        }
    }
}