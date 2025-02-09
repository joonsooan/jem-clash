using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public enum Category
    {
        Active,
        Passive
    }

    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public enum UpgradeType
    {
        UnitSpawn,
        SupplyUp,
        EnergyUp,
        UnitAuto,
        SpawnCount,
        UnitHealth,
        UnitAttack,
        BuffRange,
        UnitControl,
        Fireworks,
        Meteor,
        Blover,
        Gravity
    }

    [Header("String Data")] public string itemName;
    public Sprite itemImage;
    public UpgradeType itemType;
    public Rarity itemRarity;
    public Category itemCategory;
    [TextArea] public string description;

    [Header("Number Data")] public int maxLevel;
    public int itemPrice;
    public float[] counts;
    public int[] energyCosts;
    [Header("Cooldown")] public float cooldownTime;
}