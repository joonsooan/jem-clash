using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public UnitData unitData;
    public int isAlly; // 1 : player, -1 : enemy

    [Header("Runtime Stats")] public int health;

    public int attackDamage;
    public float moveSpeed;
    public float maxMoveSpeed;

    public void InitStats()
    {
        health = unitData.health;
        attackDamage = unitData.attackDamage;
        moveSpeed = unitData.moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}