using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int health;
    public float moveSpeed;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Debug.Log("Game Over");
    }
}