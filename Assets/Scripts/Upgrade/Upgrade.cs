using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    public int level;

    private readonly int _activeEnumStart = 8;
    private HashSet<UpgradeData.UpgradeType> _activeUpgrades;
    private bool _isShiftPressed;
    private TMP_Text _levelText;

    private void Awake()
    {
        InitializeUI();
        _activeUpgrades = InitActiveUpgrades();
    }

    private void Update()
    {
        _isShiftPressed = Input.GetKey(KeyCode.LeftShift);
    }

    public void OnEnable()
    {
        _levelText.text = $"Lv.{level:D2}";

        switch (upgradeData.type)
        {
            case UpgradeData.UpgradeType.UnitSpawn:
            case UpgradeData.UpgradeType.SupplyUp:
            case UpgradeData.UpgradeType.EnergyUp:
            case UpgradeData.UpgradeType.UnitAuto:
            case UpgradeData.UpgradeType.SpawnCount:
            case UpgradeData.UpgradeType.UnitHealth:
            case UpgradeData.UpgradeType.UnitAttack:
            case UpgradeData.UpgradeType.BuffRange:
                break;
            case UpgradeData.UpgradeType.Fireworks:
            case UpgradeData.UpgradeType.UnitControl:
            case UpgradeData.UpgradeType.Blover:
            case UpgradeData.UpgradeType.Meteor:
            case UpgradeData.UpgradeType.Gravity:
                break;
        }
    }

    private HashSet<UpgradeData.UpgradeType> InitActiveUpgrades()
    {
        var hash = new HashSet<UpgradeData.UpgradeType>();

        for (int index = _activeEnumStart; index < Enum.GetValues(typeof(UpgradeData.UpgradeType)).Length; index++)
            hash.Add((UpgradeData.UpgradeType)index);

        return hash;
    }

    private void InitializeUI()
    {
        var texts = GetComponentsInChildren<TMP_Text>();
        _levelText = texts[0];
    }

    public void OnClick()
    {
        if (_isShiftPressed) OnShiftClick();
        else OnNormalClick();
    }

    private void OnNormalClick()
    {
        // Debug.Log("Normal Click detected!");
        HandleUpgrade();
    }

    private void OnShiftClick()
    {
        // Debug.Log("Shift + Click detected!");
        HandleShiftUpgrade();
    }

    private void HandleUpgrade()
    {
        if (IsOnCooldown()) return;

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

            case UpgradeData.UpgradeType.BuffRange:
                IncreaseBuffRange();
                break;

            case UpgradeData.UpgradeType.Meteor:
                ActivateMeteor();
                break;

            case UpgradeData.UpgradeType.Blover:
                ActivateBlover();
                break;

            case UpgradeData.UpgradeType.Gravity:
                ActivateGravity();
                break;
        }

        if (_activeUpgrades.Contains(upgradeData.type))
            return;

        // 만렙이면 버튼 비활성화 (액티브 능력 제외)
        if (level == upgradeData.counts.Length)
            GetComponent<Button>().interactable = false;
    }

    private bool IsOnCooldown()
    {
        if (_activeUpgrades.Contains(upgradeData.type))
            if (CooldownManager.Instance.IsOnCooldown(upgradeData.type))
            {
                Debug.Log($"{upgradeData.type} is on cooldown.");
                return true;
            }

        return false;
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

            case UpgradeData.UpgradeType.UnitControl:
                UpgradeUnitControl();
                break;

            case UpgradeData.UpgradeType.Meteor:
                UpgradeMeteor();
                break;

            case UpgradeData.UpgradeType.Blover:
                UpgradeBlover();
                break;
        }
    }

    // 기본 능력 업그레이드

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
        GameManager.Instance.unitSpawner.spawnCount += (int)upgradeData.counts[level];
        IncrementLevel();
    }

    private void IncreaseUnitHealth()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.unitSpawner.allyData.health += (int)upgradeData.counts[level];
        IncrementLevel();
    }

    private void IncreaseUnitAttack()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.unitSpawner.allyData.attackDamage += (int)upgradeData.counts[level];
        IncrementLevel();
    }

    private void IncreaseBuffRange()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.player.GetComponentInChildren<PlayerBuff>().ChangeSprite(level);
        GameManager.Instance.player.GetComponentInChildren<PlayerBuff>().buffRadius = upgradeData.counts[level];
        IncrementLevel();
    }

    // 액티브 능력 사용

    private void ActivateFirework()
    {
        GameManager.Instance.abilityManager.GetComponent<Firework>().SetFireworkPoints();
        GameManager.Instance.abilityManager.GetComponent<Firework>().SpawnFireworks();
        StartCooldown();
    }

    private void ActivateUnitControl()
    {
        // 현재 upgradeData 데이터를 적용
        if (level == upgradeData.counts.Length)
            GameManager.Instance.abilityManager.GetComponent<UnitControl>().boostMultiplier =
                upgradeData.counts[level - 1];
        else
            GameManager.Instance.abilityManager.GetComponent<UnitControl>().boostMultiplier =
                upgradeData.counts[level];

        GameManager.Instance.abilityManager.GetComponent<UnitControl>().isUnitControl = true;
        StartCooldown();
    }

    private void ActivateBlover()
    {
        GameManager.Instance.abilityManager.GetComponent<Blover>().ActivateAbility();
        StartCooldown();
    }

    private void ActivateMeteor()
    {
        ActivateStrangeAbility<Meteor>(upgradeData);
    }

    private void ActivateGravity()
    {
        ActivateStrangeAbility<Gravity>(upgradeData);
    }

    private void ActivateStrangeAbility<T>(UpgradeData data) where T : MonoBehaviour, IStrangeAbility
    {
        T ability = GameManager.Instance.abilityManager.GetComponent<T>();

        if (ability.IsActive)
            ability.CancelAbility();
        else
            ability.ActivateAbility(data);
    }

    // 액티브 능력 업그레이드

    private void UpgradeFirework()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.abilityManager.GetComponent<Firework>().unitCount += (int)upgradeData.counts[level];
        IncrementLevel();
    }

    private void UpgradeUnitControl()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.abilityManager.GetComponent<UnitControl>().controlTime += upgradeData.counts[level];
        IncrementLevel();
    }

    private void UpgradeMeteor()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.abilityManager.GetComponent<Meteor>().damageAmount += (int)upgradeData.counts[level];
        IncrementLevel();
    }

    private void UpgradeBlover()
    {
        if (!EnoughEnergy()) return;

        SpendEnergy();
        GameManager.Instance.abilityManager.GetComponent<Blover>().blowMagnitude = upgradeData.counts[level];
        IncrementLevel();
    }

    // 기본 함수

    private void StartCooldown()
    {
        StartCoroutine(CooldownManager.Instance.StartCoolDown(upgradeData));
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
        _levelText.text = $"Lv.{level:D2}";
    }
}

public interface IStrangeAbility
{
    bool IsActive { get; }
    void CancelAbility();
    void ActivateAbility(UpgradeData upgradeData);
}