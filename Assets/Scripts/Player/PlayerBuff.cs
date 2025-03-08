using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    public float buffRadius;
    public Sprite[] sprites;

    private CircleCollider2D _circleCollider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprites[0];
    }

    private void Update()
    {
        _circleCollider.radius = buffRadius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ActivateUnitControl(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        ActivateUnitControl(other);
    }

    private void ActivateUnitControl(Collider2D other)
    {
        if (!other.CompareTag("Unit")) return;

        GameManager.instance.abilityManager.GetComponent<UnitControl>().ActivateUnitControl(other);
    }

    public void ChangeSprite(int level)
    {
        _spriteRenderer.sprite = sprites[level + 1]; // 초기 스프라이트 때문에 +1
    }
}