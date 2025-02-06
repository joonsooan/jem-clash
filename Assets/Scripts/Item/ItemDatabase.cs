using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObjects/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<UpgradeData> items;

    public UpgradeData GetItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }
}