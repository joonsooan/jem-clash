using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Header("Resources")] public int supply;

    public int energy;
    public int resourceInterval = 1;
    public int supplyAmount = 2;
    public int energyAmount = 2;

    private TMP_Text _energyText;
    private TMP_Text _supplyText;

    private void Awake()
    {
        supply = 0;
        energy = 0;
    }

    private void Start()
    {
        _supplyText = GameManager.Instance.supplyText.GetComponent<TMP_Text>();
        _energyText = GameManager.Instance.energyText.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _supplyText.text = $"Supply : {supply.ToString()}";
        _energyText.text = $"Energy : {energy.ToString()}";
    }

    public void AddSupply(int amount)
    {
        supply += amount;
        // Debug.Log($"자원 {amount} 획득");
    }

    public void AddEnergy(int amount)
    {
        energy += amount;
        // Debug.Log($"에너지 {amount} 획득");
    }

    public void SpendSupply(int amount)
    {
        if (supply < amount) return;

        supply -= amount;
        // Debug.Log($"자원 {amount} 사용. 남은 자원 : {supply}");
    }

    public void SpendEnergy(int amount)
    {
        if (energy < amount) return;

        energy -= amount;
        // Debug.Log($"에너지 {amount} 사용. 남은 에너지 : {supply}");
    }

    public void SupplyAmountUp(int amount)
    {
        supplyAmount += amount;
    }

    public void EnergyAmountUp(int amount)
    {
        energyAmount += amount;
    }
}