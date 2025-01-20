using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    public int level;

    private Image _icon;
    private TMP_Text _textDescription;
    private TMP_Text _textLevel;
    private TMP_Text _textName;

    private void Awake()
    {
        InitializeUI();
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

    private void InitializeUI()
    {
        _icon = GetComponentsInChildren<Image>()[1];
        _icon.sprite = upgradeData.upgradeIcon;

        var texts = GetComponentsInChildren<TMP_Text>();
        _textName = texts[0];
        // textLevel = texts[1];
        // textDescription = texts[2];
    }

    public void OnClick()
    {
        switch (upgradeData.type)
        {
            case UpgradeData.UpgradeType.UnitSpawn:
                SpawnAllyUnit();
                IncrementLevel();
                break;

            case UpgradeData.UpgradeType.SupplyUp:
                IncreaseSupply();
                IncrementLevel();
                break;

            case UpgradeData.UpgradeType.EnergyUp:
                IncreaseEnergy();
                IncrementLevel();
                break;

            case UpgradeData.UpgradeType.UnitAuto:
                ToggleAutoSpawn();
                break;

            case UpgradeData.UpgradeType.SpawnCount:
                IncreaseSpawnCount();
                IncrementLevel();
                break;

            case UpgradeData.UpgradeType.UnitHealth:
                IncreaseUnitHealth();
                IncrementLevel();
                break;

            case UpgradeData.UpgradeType.UnitAttack:
                IncreaseUnitAttack();
                IncrementLevel();
                break;
        }

        if (upgradeData.type != UpgradeData.UpgradeType.UnitAuto)
            if (level == upgradeData.counts.Length)
                GetComponent<Button>().interactable = false;
    }

    private void IncrementLevel()
    {
        level++;
    }

    private void IncreaseUnitAttack()
    {
        GameManager.Instance.unitSpawner.allyData.attackDamage += upgradeData.counts[level];
    }

    private void IncreaseUnitHealth()
    {
        GameManager.Instance.unitSpawner.allyData.health += upgradeData.counts[level];
    }

    private void IncreaseSpawnCount()
    {
        GameManager.Instance.unitSpawner.spawnCount += upgradeData.counts[level];
    }

    private static void ToggleAutoSpawn()
    {
        GameManager.Instance.unitSpawner.SetAutoSpawn(
            !GameManager.Instance.unitSpawner.isAutoSpawn);
    }

    private void IncreaseEnergy()
    {
        GameManager.Instance.resourceManager.EnergyAmountUp(
            upgradeData.counts[level]);
    }

    private void IncreaseSupply()
    {
        GameManager.Instance.resourceManager.SupplyAmountUp(
            upgradeData.counts[level]);
    }

    private static void SpawnAllyUnit()
    {
        GameManager.Instance.unitSpawner.SpawnAllyUnit(
            GameManager.Instance.unitSpawner.spawnCount);
    }
}