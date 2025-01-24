using UnityEngine;
using Random = UnityEngine.Random;

public class UnitMovement : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;

    private Vector2 _currentDirection;
    private UnitStats _stats;
    private UnitCollision _unitCollision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<UnitStats>();
        _unitCollision = GetComponent<UnitCollision>();
    }

    private void Start()
    {
        switch (_unitCollision.isAlly)
        {
            case 1:
                target = GameManager.Instance.enemyNexus;
                break;
            case -1:
                target = GameManager.Instance.playerNexus;
                break;
        }
    }

    private void OnEnable()
    {
        MoveInRandomDirection();
    }

    private void MoveInRandomDirection()
    {
        Vector2 randomVec = new Vector2(
            Random.Range(0f, 1f) * _unitCollision.isAlly, Random.Range(-0.5f, 0.5f)).normalized;
        // Debug.Log(_stats.moveSpeed);
        rb.velocity = randomVec * _stats.moveSpeed;
    }

    public void HeadToEnemyNexus()
    {
        Vector2 dirVec = (target.position - transform.position).normalized;
        rb.velocity = dirVec * _stats.moveSpeed;
    }
}