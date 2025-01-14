using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Vector2 currentDirection;
    private Rigidbody2D rb;
    private UnitStats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<UnitStats>();
    }

    private void Start()
    {
        Vector2 randomVec = new Vector2(
            Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.AddForce(randomVec * stats.moveSpeed, ForceMode2D.Impulse);
    }
}