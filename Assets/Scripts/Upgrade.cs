using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    public int level;

    private Image icon;
    private TMP_Text textDescription;
    private TMP_Text textLevel;
    private TMP_Text textName;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = upgradeData.upgradeIcon;
        var texts = GetComponentsInChildren<TMP_Text>();
        textName = texts[0];
        // textLevel = texts[1];
        // textDescription = texts[2];
    }

    public void OnEnable()
    {
        // textLevel.text = $"Lv. {level}";

        switch (upgradeData.type)
        {
            case UpgradeData.UpgradeType.UnitSpawn:
                break;
            case UpgradeData.UpgradeType.SupplyUp:
                break;
            case UpgradeData.UpgradeType.EnergyUp:
                break;
            case UpgradeData.UpgradeType.UnitAuto:
                break;
            case UpgradeData.UpgradeType.SpawnCount:
                break;
            case UpgradeData.UpgradeType.UnitHealth:
                break;
        }
    }

    public void OnClick()
    {
        switch (upgradeData.type)
        {
            case UpgradeData.UpgradeType.UnitSpawn:
                GameManager.Instance.unitSpawner.SpawnAllyUnit(
                    GameManager.Instance.unitSpawner.spawnCount);
                level++;
                break;

            case UpgradeData.UpgradeType.SupplyUp:
                GameManager.Instance.resourceManager.SupplyAmountUp(
                    upgradeData.counts[level]);
                level++;
                break;

            case UpgradeData.UpgradeType.EnergyUp:
                GameManager.Instance.resourceManager.EnergyAmountUp(
                    upgradeData.counts[level]);
                level++;
                break;

            case UpgradeData.UpgradeType.UnitAuto:
                GameManager.Instance.unitSpawner.SetAutoSpawn(!GameManager.Instance.unitSpawner.isAutoSpawn);
                break;

            case UpgradeData.UpgradeType.SpawnCount:
                GameManager.Instance.unitSpawner.spawnCount += upgradeData.counts[level];
                level++;
                break;

            case UpgradeData.UpgradeType.UnitHealth:
                break;
        }

        if (upgradeData.type != UpgradeData.UpgradeType.UnitAuto)
            if (level == upgradeData.counts.Length)
                GetComponent<Button>().interactable = false;
    }
}