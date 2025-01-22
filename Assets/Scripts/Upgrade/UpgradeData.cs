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
        UnitAttack,
        Fireworks,
        UnitControl
    }

    public UpgradeType type;
    [TextArea] public string upgradeDescription;

    public int[] counts;
    public int[] energyCosts;
}