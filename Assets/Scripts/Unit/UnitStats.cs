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
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {health}");

        if (health <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Unit Destroyed");
        gameObject.SetActive(false);
    }
}