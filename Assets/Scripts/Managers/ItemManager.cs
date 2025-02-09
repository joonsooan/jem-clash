using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject itemBtnPrefab;
    public List<UpgradeData> items = new();

    public void CreateItemBtn(UpgradeData data)
    {
        if (data == null) return;

        GameObject buttonObj = Instantiate(itemBtnPrefab, transform);
        Upgrade upgrade = buttonObj.GetComponent<Upgrade>();
        
        if (upgrade != null)
        {
            upgrade.upgradeData = data;
            buttonObj.GetComponent<Image>().sprite = upgrade.upgradeData.itemImage;
            upgrade.level = 0;

            Button btn = buttonObj.GetComponent<Button>();
            btn.onClick.AddListener(upgrade.OnClick);
        }
    }
}