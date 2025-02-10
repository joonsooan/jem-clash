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
    public string itemCategory;
    public string imagePath;
    public string description;
    public int maxLevel;
    public int itemPrice;
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
            Debug.LogError("JSON 파일을 찾을 수 없습니다");
            return;
        }

        string json = File.ReadAllText(path);
        ItemDataList itemList = JsonUtility.FromJson<ItemDataList>(json);

        const string databasePath = "Assets/SO/ItemDatabase.asset";
        ItemDatabase itemDatabase = AssetDatabase.LoadAssetAtPath<ItemDatabase>(databasePath);
        if (itemDatabase == null)
        {
            Debug.LogError("ItemDatabase를 찾을 수 없습니다");
            return;
        }

        itemDatabase.items.Clear();

        foreach (ItemDataJSON item in itemList.items)
        {
            UpgradeData newItem = ScriptableObject.CreateInstance<UpgradeData>();

            newItem.itemName = item.itemName;
            newItem.itemType = (UpgradeData.UpgradeType)Enum.Parse(typeof(UpgradeData.UpgradeType), item.itemType);
            newItem.itemRarity = (UpgradeData.Rarity)Enum.Parse(typeof(UpgradeData.Rarity), item.itemRarity);
            newItem.itemCategory = (UpgradeData.Category)Enum.Parse(typeof(UpgradeData.Category), item.itemCategory);
            newItem.itemImage = Resources.Load<Sprite>(item.imagePath);
            newItem.description = item.description;

            newItem.maxLevel = item.maxLevel;
            newItem.itemPrice = item.itemPrice;
            newItem.counts = item.counts;
            newItem.energyCosts = item.energyCosts;
            newItem.cooldownTime = item.cooldownTime;

            AssetDatabase.CreateAsset(newItem, $"Assets/SO/Items/{item.itemName}.asset");
            itemDatabase.items.Add(newItem);
        }

        EditorUtility.SetDirty(itemDatabase); // 변경 사항 감지
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}