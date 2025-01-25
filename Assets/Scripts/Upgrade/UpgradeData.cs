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
        UnitControl,
        BuffRange
    }

    public UpgradeType type;
    [TextArea] public string upgradeDescription;

    public float[] counts;
    public int[] energyCosts;
}