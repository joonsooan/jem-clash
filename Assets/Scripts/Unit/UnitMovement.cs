using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private UnitStats stats;

    private void Awake()
    {
        stats = GetComponent<UnitStats>();
        rb = GetComponent<Rigidbody2D>();
    }
}