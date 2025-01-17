using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    public float buffRadius;

    // private Color _color; // 버그가 있긴 한데, 일단 중요한건 아니니까 무시

    private void Update()
    {
        transform.localScale = new Vector3(buffRadius, buffRadius, buffRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Unit")) return;
        Debug.Log("Unit entered");
        // _color = other.GetComponent<SpriteRenderer>().color;
        // other.GetComponent<SpriteRenderer>().color = Color.green;

        UnitCollision unitCollision = other.GetComponent<UnitCollision>();
        if (unitCollision.isAlly == -1) return; // 적군이면 리턴

        UnitMovement unitMovement = other.GetComponent<UnitMovement>();
        unitMovement.MoveInRandomDirection();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Unit")) return;
        Debug.Log("Unit exited");
        // other.GetComponent<SpriteRenderer>().color = _color;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Unit")) return;
        Debug.Log("Unit is in buff zone");
    }
}