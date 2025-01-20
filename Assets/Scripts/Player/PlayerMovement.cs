using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement;
    private float moveSpeed;
    private Rigidbody2D rb;
    private PlayerStats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * stats.moveSpeed;
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
}