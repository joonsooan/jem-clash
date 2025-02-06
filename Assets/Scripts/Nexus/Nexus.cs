using TMPro;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    public int isAlly; // 1 : player, -1 : enemy
    public int health;
    public int attackDamage;

    private TMP_Text _healthText;

    private void Awake()
    {
        _healthText = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        _healthText.text = health.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObj = collision.gameObject;
        if (!otherObj.CompareTag("Unit")) return;

        UnitStats otherStats = otherObj.GetComponent<UnitStats>();
        if (isAlly == otherStats.isAlly) return;

        // Debug.Log("Nexus Hit");
        health -= otherStats.attackDamage;
        // 1안. 유닛과 넥서스가 충돌하면 유닛 사망
        otherStats.Die();
        // 2안. 유닛과 넥서스가 충돌하면 유닛 체력 깎임
        // otherObj.GetComponent<UnitStats>().TakeDamage(attackDamage);

        if (health <= 0)
            GameManager.Instance.GameWin();
    }
}