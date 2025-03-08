using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public List<UpgradeData> items = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(UpgradeData data)
    {
        items.Add(data);
    }

    public void RemoveItem(UpgradeData data)
    {
        items.Remove(data);
    }
}