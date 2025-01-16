using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    public float buffRadius;

    private Color _color;

    private void Update()
    {
        transform.localScale = new Vector3(buffRadius, buffRadius, buffRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Unit")) return;
        Debug.Log("Unit entered");
        _color = other.GetComponent<SpriteRenderer>().color;
        other.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Unit")) return;
        Debug.Log("Unit exited");
        other.GetComponent<SpriteRenderer>().color = _color;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Unit")) return;
        Debug.Log("Unit is in buff zone");
    }
}