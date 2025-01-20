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
                break;

            case UpgradeData.UpgradeType.SupplyUp:
                IncreaseSupply();
                break;

            case UpgradeData.UpgradeType.EnergyUp:
                IncreaseEnergy();
                break;

            case UpgradeData.UpgradeType.UnitAuto:
                ToggleAutoSpawn();
                break;

            case UpgradeData.UpgradeType.SpawnCount:
                IncreaseSpawnCount();
                break;

            case UpgradeData.UpgradeType.UnitHealth:
                IncreaseUnitHealth();
                break;

            case UpgradeData.UpgradeType.UnitAttack:
                IncreaseUnitAttack();
                break;
        }

        if (level == upgradeData.counts.Length)
            GetComponent<Button>().interactable = false;
    }

    private static void SpawnAllyUnit()
    {
        GameManager.Instance.unitSpawner.SpawnAllyUnit(
            GameManager.Instance.unitSpawner.spawnCount);
    }

    private void IncreaseSupply()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.resourceManager.SupplyAmountUp(
            upgradeData.counts[level]);
        IncrementLevel();
    }

    private void IncreaseEnergy()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.resourceManager.EnergyAmountUp(
            upgradeData.counts[level]);
        IncrementLevel();
    }

    private static void ToggleAutoSpawn()
    {
        GameManager.Instance.unitSpawner.SetAutoSpawn(
            !GameManager.Instance.unitSpawner.isAutoSpawn);
    }

    private void IncreaseSpawnCount()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.unitSpawner.spawnCount += upgradeData.counts[level];
        IncrementLevel();
    }

    private void IncreaseUnitHealth()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.unitSpawner.allyData.health += upgradeData.counts[level];
        IncrementLevel();
    }

    private void IncreaseUnitAttack()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.unitSpawner.allyData.attackDamage += upgradeData.counts[level];
        IncrementLevel();
    }

    private bool EnoughEnergy()
    {
        return GameManager.Instance.resourceManager.energy >= upgradeData.energyCosts[level];
    }

    private void SpendEnergy()
    {
        GameManager.Instance.resourceManager.SpendEnergy(upgradeData.energyCosts[level]);
    }

    private void IncrementLevel()
    {
        level++;
    }
}