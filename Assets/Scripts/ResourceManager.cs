using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int supply;
    public int energy;

    private void Awake()
    {
        supply = 0;
        energy = 0;
    }

    public void AddSupply(int amount)
    {
        supply += amount;
        Debug.Log($"자원 {amount} 획득");
    }

    public void AddEnergy(int amount)
    {
        energy += amount;
        Debug.Log($"에너지 {amount} 획득");
    }

    public void SpendSupply(int amount)
    {
        if (supply < amount) return;

        supply -= amount;
        Debug.Log($"자원 {amount} 사용. 남은 자원 : {supply}");
    }

    public void SpendEnergy(int amount)
    {
        if (energy < amount) return;

        energy -= amount;
        Debug.Log($"에너지 {amount} 사용. 남은 에너지 : {supply}");
    }
}