using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public float moveSpeed;
    private int attackDamage;
    private int buffRange;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) GameOver();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}