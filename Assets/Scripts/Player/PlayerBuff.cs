using System.Collections;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    public float buffRadius;
    public bool isUnitControl;
    public float boostMultiplier;
    public Sprite[] sprites;

    private CircleCollider2D _circleCollider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        isUnitControl = false;
        _circleCollider = GetComponent<CircleCollider2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprites[0];
    }

    private void Update()
    {
        _circleCollider.radius = buffRadius;
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
        UnitStats otherStats = other.GetComponent<UnitStats>();
        if (otherStats.isAlly == -1) return; // 적군이면 리턴

        if (!isUnitControl) return;

        // 상대 넥서스 방향으로 이동
        UnitMovement unitMovement = other.GetComponent<UnitMovement>();
        UnitControl unitControl = GameManager.Instance.abilityManager.GetComponent<UnitControl>();
        unitMovement.HeadToEnemyNexus();

        StartCoroutine(BoostUnitSpeed(unitMovement, boostMultiplier, unitControl.controlTime));
    }

    private IEnumerator BoostUnitSpeed(UnitMovement unitMovement, float mult, float controlTime)
    {
        float originalSpeed = unitMovement.rb.velocity.magnitude;
        unitMovement.rb.velocity *= mult;

        yield return new WaitForSeconds(controlTime);

        Vector2 currentVec = unitMovement.rb.velocity.normalized;
        unitMovement.rb.velocity = currentVec * originalSpeed;
    }

    public void ChangeSprite(int level)
    {
        _spriteRenderer.sprite = sprites[level + 1]; // 초기 스프라이트 때문에 +1
    }
}