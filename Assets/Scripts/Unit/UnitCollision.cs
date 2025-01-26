using UnityEngine;

public class UnitCollision : MonoBehaviour
{
    private UnitStats _stats;

    private void Awake()
    {
        _stats = GetComponent<UnitStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObj = collision.gameObject;
        if (!otherObj.CompareTag("Unit")) return; // 유닛이 아니면 리턴

        UnitCollision otherCollision = otherObj.GetComponent<UnitCollision>();
        if (_stats.isAlly == otherCollision._stats.isAlly) return; // 아군일 경우 리턴

        UnitStats otherStats = otherObj.GetComponent<UnitStats>();

        _stats.TakeDamage(otherStats.attackDamage); // 데미지 계산
    }
}