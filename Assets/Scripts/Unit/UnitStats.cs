using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public UnitData data;
    public int health;
    public int attackDamage;
    public float moveSpeed;

    private void Start()
    {
        InitStats();
    }

    public void InitStats()
    {
        health = data.health;
        attackDamage = data.attackDamage;
        moveSpeed = data.moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}