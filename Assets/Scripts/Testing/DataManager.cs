using System.IO;
using UnityEngine;

public class PlayerData
{
    public int coin = 100;
    public int item = -1;
    public int level = 1;
    public string name;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public string path;
    public int nowSlot;

    public PlayerData nowPlayer = new();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this) Destroy(instance.gameObject);
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/save";
        print(path);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}