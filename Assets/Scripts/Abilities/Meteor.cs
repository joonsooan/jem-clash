using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Meteor : MonoBehaviour
{
    public GameObject rangePrefab;
    public LayerMask targetLayer;

    [Header("Values")] public float radius;

    public int damageAmount;
    public float meteorDropDelay;
    public bool isActive;

    private GameObject _rangeIndicator;

    private UpgradeData _upgradeData;

    private void Update()
    {
        Function();
    }

    private void OnDrawGizmosSelected()
    {
        if (isActive)
        {
            Gizmos.color = Color.green;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Gizmos.DrawWireSphere(new Vector3(mousePos.x, mousePos.y, 0), radius);
        }
    }

    private void Function()
    {
        if (isActive && _rangeIndicator != null) FollowMouse();

        if (isActive && Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) // UI를 클릭한 경우
            {
                Debug.Log("UI 클릭");
                return;
            }

            Vector2 mousePos = GetMousePos();
            StartCoroutine(DeactivateAbility(meteorDropDelay, mousePos));
            CooldownManager.Instance.StartCoroutine(CooldownManager.Instance.StartCoolDown(_upgradeData));
        }
    }

    public void ActivateAbility(UpgradeData upgradeData)
    {
        if (isActive) return;

        isActive = true;
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
        isActive = false;

        if (_rangeIndicator != null)
            Destroy(_rangeIndicator);
    }

    private IEnumerator DeactivateAbility(float delay, Vector2 mousePos)
    {
        isActive = false; // 비활성화해서 이후 클릭을 방지

        SpriteRenderer sr = _rangeIndicator.GetComponent<SpriteRenderer>();
        sr.color = Color.red;

        yield return new WaitForSeconds(delay);

        DealDamage(mousePos);

        if (_rangeIndicator != null)
            Destroy(_rangeIndicator);
    }

    private void FollowMouse()
    {
        Vector3 mousePos = GetMousePos();
        _rangeIndicator.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }

    private void DealDamage(Vector2 position)
    {
        var hitColliders = Physics2D.OverlapCircleAll(position, radius, targetLayer);

        foreach (Collider2D coll in hitColliders)
        {
            UnitStats unitStats = coll.gameObject.GetComponent<UnitStats>();

            if (unitStats != null && unitStats.isAlly == -1) // 적군일 때 
                unitStats.TakeDamage(damageAmount);
        }
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePos;
    }
}