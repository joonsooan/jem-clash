using UnityEngine;
using Random = UnityEngine.Random;

public class UnitMovement : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;

    private Vector2 _currentDirection;
    private UnitStats _stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<UnitStats>();
    }

    private void Start()
    {
        switch (_stats.isAlly)
        {
            case 1:
                target = GameManager.Instance.enemyNexus;
                break;
            case -1:
                target = GameManager.Instance.playerNexus;
                break;
        }
    }

    private void FixedUpdate()
    {
        LimitSpeed();
    }

    private void OnEnable()
    {
        MoveInRandFrontDir();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slow Zone") && _stats.isAlly == -1)
            InitSpeed();
    }

    private void InitSpeed()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, rb.velocity.normalized * _stats.moveSpeed, 0.5f);
    }

    private void LimitSpeed()
    {
        if (rb.velocity.magnitude > _stats.maxMoveSpeed)
            rb.velocity = rb.velocity.normalized * _stats.maxMoveSpeed;
    }

    private void MoveInRandFrontDir()
    {
        Vector2 randomVec = new Vector2(
            Random.Range(0f, 1f) * _stats.isAlly, Random.Range(-0.5f, 0.5f)).normalized;
        rb.velocity = randomVec * _stats.moveSpeed;
    }

    private void MoveInRandDir()
    {
        Vector2 randomVec = new Vector2(
            Random.Range(-1f, 1f) * _stats.isAlly, Random.Range(-1f, 1f)).normalized;
        rb.velocity = randomVec * _stats.moveSpeed;
    }

    public void HeadToEnemyNexus()
    {
        Vector2 dirVec = (target.position - transform.position).normalized;
        rb.velocity = dirVec * _stats.moveSpeed;
    }

    public void Blover(float magnitude)
    {
        rb.AddForce(new Vector2(magnitude * 0.1f, 0), ForceMode2D.Impulse);
    }

    public void GravityPull(Vector2 targetPos)
    {
        Vector2 dirVec = targetPos - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        rb.AddForce(dirVec * (GameManager.Instance.abilityManager.GetComponent<Gravity>().gravityForce * 0.1f),
            ForceMode2D.Impulse);
    }
}