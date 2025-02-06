using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ItemDataList
{
    public List<ItemDataJSON> items;
}

[Serializable]
public class ItemDataJSON
{
    public string itemName;
    public string itemType;
    public string itemRarity;
    public string imagePath;
    public string description;
    public int maxLevel;
    public float[] counts;
    public int[] energyCosts;
    public float cooldownTime;
}

public class ItemImporter : MonoBehaviour
{
    [MenuItem("Tools/Import Items from JSON")]
    public static void ImportItems()
    {
        string path = "Assets/Resources/items.json";
        if (!File.Exists(path))
        {
            Debug.LogError("JSON 파일을 찾을 수 없습니다!");
            return;
        }

        string json = File.ReadAllText(path);
        ItemDataList itemList = JsonUtility.FromJson<ItemDataList>(json);

        foreach (ItemDataJSON item in itemList.items)
        {
            UpgradeData newItem = ScriptableObject.CreateInstance<UpgradeData>();

            newItem.itemName = item.itemName;
            newItem.itemType = (UpgradeData.UpgradeType)Enum.Parse(typeof(UpgradeData.UpgradeType), item.itemType);
            newItem.itemRarity = (UpgradeData.Rarity)Enum.Parse(typeof(UpgradeData.Rarity), item.itemRarity);
            newItem.itemImage = Resources.Load<Sprite>(item.imagePath);
            newItem.description = item.description;

            newItem.maxLevel = item.maxLevel;
            newItem.counts = item.counts;
            newItem.energyCosts = item.energyCosts;
            newItem.cooldownTime = item.cooldownTime;

            AssetDatabase.CreateAsset(newItem, $"Assets/SO/Items/{item.itemName}.asset");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("데이터를 ScriptableObject로 변환 완료");
    }
}