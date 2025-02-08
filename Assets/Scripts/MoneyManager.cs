using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int playerMoney;
    public TMP_Text playerMoneyText;
    public static MoneyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdatePlayerMoney();
    }

    private void UpdatePlayerMoney()
    {
        playerMoneyText.text = playerMoney.ToString();
    }

    public int GetMoney()
    {
        return playerMoney;
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        UpdatePlayerMoney();
    }

    public bool SubtractMoney(int amount)
    {
        if (amount > playerMoney)
        {
            Debug.Log("Not enough money");
            return false;
        }

        playerMoney -= amount;
        UpdatePlayerMoney();
        return true;
    }
}