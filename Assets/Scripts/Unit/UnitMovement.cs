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

}