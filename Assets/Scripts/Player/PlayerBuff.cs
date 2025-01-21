using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    public float buffRadius;
    public bool isUnitControl;

    private void Awake()
    {
        isUnitControl = false;
    }

    private void Update()
    {
        transform.localScale = new Vector3(buffRadius, buffRadius, buffRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Unit")) return;

        ActivateUnitControl(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Unit")) return;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Unit")) return;

        ActivateUnitControl(other);
    }

    private void ActivateUnitControl(Collider2D other)
    {
        UnitCollision unitCollision = other.GetComponent<UnitCollision>();
        if (unitCollision.isAlly == -1) return; // 적군이면 리턴

        if (!isUnitControl) return;

        // 상대 넥서스 방향으로 이동
        UnitMovement unitMovement = other.GetComponent<UnitMovement>();
        unitMovement.HeadToEnemyNexus();
    }
}