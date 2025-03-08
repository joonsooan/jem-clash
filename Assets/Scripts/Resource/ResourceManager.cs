using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    [Header("Resources")] public int supply;

    public int energy;
    public int resourceInterval = 1;
    public int supplyAmount = 2;
    public int energyAmount = 2;

    private TMP_Text _energyText;
    private TMP_Text _supplyText;

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

        supply = 0;
        // energy = 0;
    }

    private void Start()
    {
        _supplyText = GameManager.instance.supplyText.GetComponent<TMP_Text>();
        _energyText = GameManager.instance.energyText.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _supplyText.text = $"{supply.ToString()}";
        _energyText.text = $"{energy.ToString()}";
    }

    public void AddSupply(float amount)
    {
        supply += (int)amount;
        // Debug.Log($"자원 {amount} 획득");
    }

    public void AddEnergy(float amount)
    {
        energy += (int)amount;
        // Debug.Log($"에너지 {amount} 획득");
    }

    public void SpendSupply(float amount)
    {
        if (supply < amount) return;

        supply -= (int)amount;
        // Debug.Log($"자원 {amount} 사용. 남은 자원 : {supply}");
    }

    public void SpendEnergy(float amount)
    {
        if (energy < amount) return;

        energy -= (int)amount;
        // Debug.Log($"에너지 {amount} 사용. 남은 에너지 : {supply}");
    }

    public void SupplyAmountUp(float amount)
    {
        supplyAmount += (int)amount;
    }

    public void EnergyAmountUp(float amount)
    {
        energyAmount += (int)amount;
    }
}