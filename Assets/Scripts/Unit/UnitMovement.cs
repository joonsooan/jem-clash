using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Vector2 _currentDirection;
    private Rigidbody2D _rb;
    private UnitStats _stats;
    private UnitCollision _unitCollision;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<UnitStats>();
        _unitCollision = GetComponent<UnitCollision>();
    }

    private void OnEnable()
    {
        MoveInRandomDirection();
    }

    public void MoveInRandomDirection()
    {
        Vector2 randomVec = new Vector2(
            Random.Range(0f, 1f) * _unitCollision.isAlly, Random.Range(-0.5f, 0.5f)).normalized;
        Debug.Log(_stats.moveSpeed);
        _rb.velocity = randomVec * _stats.moveSpeed;
    }
}