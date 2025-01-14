using UnityEngine;

public class UnitCollision : MonoBehaviour
{
    public bool isAlly; // 아군 적군 구분
    private Rigidbody2D rb;
    private UnitStats stats;
    private UnitMovement unitMovement;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        stats = GetComponent<UnitStats>();
        unitMovement = GetComponent<UnitMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObj = collision.gameObject;

        // 유닛이 아니면 리턴
        if (!otherObj.CompareTag("Unit")) return;

        UnitStats otherStats = otherObj.GetComponent<UnitStats>();
        UnitCollision otherCollision = otherObj.GetComponent<UnitCollision>();

        // 아군일 경우 리턴
        if (isAlly == otherCollision.isAlly) return;

        // 데미지 계산
        stats.TakeDamage(otherStats.attackDamage);
    }
}