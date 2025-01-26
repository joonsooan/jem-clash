using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public UnitData unitData;
    public int isAlly; // 1 : player, -1 : enemy

    [Header("Runtime Stats")] public int health;

    public int attackDamage;
    public float moveSpeed;

    private int _baseAttackDamage;
    private int _baseHealth;
    private float _baseMoveSpeed;

    // private void Awake()
    // {
    //     CacheBaseStats();
    // }
    //
    // private void CacheBaseStats()
    // {
    //     _baseHealth = unitData.health;
    //     _baseAttackDamage = unitData.attackDamage;
    //     _baseMoveSpeed = unitData.moveSpeed;
    // }

    public void InitStats()
    {
        health = unitData.health;
        attackDamage = unitData.attackDamage;
        moveSpeed = unitData.moveSpeed;
    }

    // public void ResetStats()
    // {
    //     unitData.health = _baseHealth;
    //     unitData.attackDamage = _baseAttackDamage;
    //     unitData.moveSpeed = _baseMoveSpeed;
    //
    //     InitStats();
    // }

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