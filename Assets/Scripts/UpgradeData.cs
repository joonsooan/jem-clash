using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public enum UpgradeType
    {
        UnitSpawn,
        SupplyUp,
        EnergyUp,
        UnitAuto,
        SpawnCount,
        UnitHealth,
        UnitAttack
    }

    public UpgradeType type;
    public Sprite upgradeIcon;
    [TextArea] public string upgradeDescription;

    public int baseCount;
    public int[] counts;
}