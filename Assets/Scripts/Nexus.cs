using TMPro;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    public bool isAlly;
    public int health;
    public int attackDamage;

    private TMP_Text healthText;

    private void Awake()
    {
        healthText = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        healthText.text = health.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObj = collision.gameObject;
        if (!otherObj.CompareTag("Unit")) return;

        UnitCollision otherCollision = otherObj.GetComponent<UnitCollision>();
        if (isAlly == otherCollision.isAlly) return;

        Debug.Log("Nexus Hit");
        health -= otherObj.GetComponent<UnitStats>().attackDamage;
        // 1안. 유닛과 넥서스가 충돌하면 유닛 사망
        otherObj.GetComponent<UnitStats>().Die();
        // 2안. 유닛과 넥서스가 충돌하면 유닛 체력 깎임
        // otherObj.GetComponent<UnitStats>().TakeDamage(attackDamage);

        if (health <= 0) Die();
    }

    private void Die()
    {
        // 넥서스 깨짐 -> 게임 오버
        Debug.Log("Game Over");
    }
}