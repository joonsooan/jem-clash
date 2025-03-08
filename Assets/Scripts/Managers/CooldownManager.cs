using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    public static CooldownManager instance;
    private readonly Dictionary<UpgradeData.UpgradeType, bool> _cooldowns = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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