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
        UnitHealth
    }

    public Sprite upgradeIcon;
    [TextArea] public string upgradeDescription;

    public int baseCount;
    public int[] counts;
}