using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    private readonly Dictionary<UpgradeData.UpgradeType, bool> _cooldowns = new();

    public static CooldownManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool IsOnCooldown(UpgradeData.UpgradeType upgradeType)
    {
        if (_cooldowns.ContainsKey(upgradeType))
            return _cooldowns[upgradeType];

        return false;
    }

    public IEnumerator StartCoolDown(UpgradeData upgradeData)
    {
        _cooldowns[upgradeData.itemType] = true;

        yield return new WaitForSeconds(upgradeData.cooldownTime);

        _cooldowns[upgradeData.itemType] = false;
        Debug.Log($"{upgradeData.itemType} is ready to use again!");
    }
}