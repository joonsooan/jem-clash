using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    public int level;

    private Image _icon;

    private bool _isShiftPressed;
    private TMP_Text _textDescription;
    private TMP_Text _textLevel;
    private TMP_Text _textName;

    private void Awake()
    {
        InitializeUI();
    }

    private void Update()
    {
        _isShiftPressed = Input.GetKey(KeyCode.LeftShift);
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
            case UpgradeData.UpgradeType.Fireworks:
                break;
            case UpgradeData.UpgradeType.UnitControl:
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
        if (_isShiftPressed) OnShiftClick();
        else OnNormalClick();
    }

    private void OnNormalClick()
    {
        Debug.Log("Normal Click detected!");
        HandleUpgrade();
    }

    private void OnShiftClick()
    {
        Debug.Log("Shift + Click detected!");
        HandleShiftUpgrade();
    }

    private void HandleUpgrade()
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

            case UpgradeData.UpgradeType.Fireworks:
                ActivateFirework();
                break;

            case UpgradeData.UpgradeType.UnitControl:
                ActivateUnitControl();
                break;
        }

        if (upgradeData.type == UpgradeData.UpgradeType.Fireworks) return;

        if (level == upgradeData.counts.Length)
            GetComponent<Button>().interactable = false;
    }

    private void HandleShiftUpgrade()
    {
        if (level == upgradeData.counts.Length)
        {
            Debug.Log("Upgrade is already at max level.");
            return;
        }

        switch (upgradeData.type)
        {
            case UpgradeData.UpgradeType.Fireworks:
                UpgradeFirework();
                break;
        }
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

    private void ActivateFirework()
    {
        GameManager.Instance.abilityManager.GetComponent<Firework>().SetFireworkPoints();
        GameManager.Instance.abilityManager.GetComponent<Firework>().SpawnFireworks();
    }

    private void ActivateUnitControl()
    {
        GameManager.Instance.player.GetComponentInChildren<PlayerBuff>().isUnitControl = true;
        StartCoroutine(DeactivateUnitControlAfterDelay(
            GameManager.Instance.abilityManager.GetComponent<UnitControl>().controlTime));
    }

    private IEnumerator DeactivateUnitControlAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.player.GetComponentInChildren<PlayerBuff>().isUnitControl = false;
    }

    private void UpgradeFirework()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.abilityManager.GetComponent<Firework>().unitCount += upgradeData.counts[level];
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