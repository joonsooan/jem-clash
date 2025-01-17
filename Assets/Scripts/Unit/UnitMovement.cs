using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Vector2 _currentDirection;
    private Rigidbody2D _rb;
    private UnitStats _stats;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<UnitStats>();
    }

    private void OnEnable()
    {
        MoveInRandomDirection();
    }

    private void MoveInRandomDirection()
    {
        Vector2 randomVec = new Vector2(
            Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Debug.Log(_stats.moveSpeed);
        _rb.velocity = randomVec * _stats.moveSpeed;
        // _rb.AddForce(randomVec * _stats.moveSpeed, ForceMode2D.Impulse);
    }
}