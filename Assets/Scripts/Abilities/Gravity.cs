using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gravity : MonoBehaviour, IStrangeAbility
{
    public GameObject rangePrefab;
    public LayerMask targetLayer;

    [Header("Values")] public float radius;
    public float controlTime;
    public float gravityInterval;
    public float gravityForce;
    private bool _isGravity;
    private GameObject _rangeIndicator;

    private UpgradeData _upgradeData;

    private void Update()
    {
        Function();
    }

    private void OnDrawGizmosSelected()
    {
        if (IsActive)
        {
            Gizmos.color = Color.green;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Gizmos.DrawWireSphere(new Vector3(mousePos.x, mousePos.y, 0), radius);
        }
    }

    public bool IsActive { get; set; }

    public void ActivateAbility(UpgradeData upgradeData)
    {
        if (IsActive) return;

        IsActive = true;
        _upgradeData = upgradeData;

        if (_rangeIndicator == null)
        {
            _rangeIndicator = Instantiate(rangePrefab);
            _rangeIndicator.transform.localPosition = new Vector3(radius, radius, 1);
        }

        FollowMouse();
    }

    public void CancelAbility()
    {
        IsActive = false;

        if (_rangeIndicator != null)
            Destroy(_rangeIndicator);
    }

    private void Function()
    {
        if (IsActive && _rangeIndicator != null) FollowMouse();

        if (IsActive && Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI 클릭");
                return;
            }

            Vector2 mousePos = GetMousePos();
            StartCoroutine(DeactivateAbility(controlTime, mousePos));
            CooldownManager.Instance.StartCoroutine(CooldownManager.Instance.StartCoolDown(_upgradeData));
        }
    }

    private IEnumerator DeactivateAbility(float delay, Vector2 targetPos)
    {
        IsActive = false; // 비활성화해서 이후 클릭을 방지
        _isGravity = true;

        SpriteRenderer sr = _rangeIndicator.GetComponent<SpriteRenderer>();
        sr.color = Color.red;

        StartCoroutine(GravityPull(targetPos));

        yield return new WaitForSeconds(delay);

        _isGravity = false;

        if (_rangeIndicator != null)
            Destroy(_rangeIndicator);
    }

    private void FollowMouse()
    {
        Vector3 mousePos = GetMousePos();
        _rangeIndicator.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }

    private IEnumerator GravityPull(Vector2 targetPos)
    {
        while (_isGravity)
        {
            ApplyGravityPull(targetPos);
            yield return new WaitForSeconds(gravityInterval);
        }

        ResetVelocity(targetPos);
    }

    private void ApplyGravityPull(Vector2 targetPos)
    {
        var hitColliders = Physics2D.OverlapCircleAll(targetPos, radius, targetLayer);

        foreach (Collider2D coll in hitColliders)
        {
            UnitStats unitStats = coll.gameObject.GetComponent<UnitStats>();

            if (unitStats != null && unitStats.isAlly == -1) // 적군일 때
            {
                Debug.Log("Pull Enemy");
                coll.gameObject.GetComponent<UnitMovement>().GravityPull(targetPos);
            }
        }
    }

    private void ResetVelocity(Vector2 targetPos)
    {
        var hitColl = Physics2D.OverlapCircleAll(targetPos, radius, targetLayer);

        foreach (Collider2D coll in hitColl)
        {
            UnitStats unitStats = coll.gameObject.GetComponent<UnitStats>();
            UnitMovement unitMovement = coll.gameObject.GetComponent<UnitMovement>();

            if (unitMovement != null && unitStats.isAlly == -1) // 적군일 때
                unitMovement.rb.velocity = unitMovement.rb.velocity.normalized * unitStats.moveSpeed;
        }
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePos;
    }
}