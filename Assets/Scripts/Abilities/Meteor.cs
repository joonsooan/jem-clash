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

    private bool _isActive;
    private GameObject _rangeIndicator;

    private void Update()
    {
        Function();
    }

    private void OnDrawGizmosSelected()
    {
        if (_isActive)
        {
            Gizmos.color = Color.green;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Gizmos.DrawWireSphere(new Vector3(mousePos.x, mousePos.y, 0), radius);
        }
    }

    private void Function()
    {
        if (_isActive && _rangeIndicator != null) FollowMouse();

        if (_isActive && Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI 클릭");
                return;
            }

            Vector2 mousePos = GetMousePosition();
            StartCoroutine(DeactivateAbility(meteorDropDelay, mousePos));
        }
    }

    public void ActivateAbility()
    {
        _isActive = true;

        if (_rangeIndicator == null)
        {
            _rangeIndicator = Instantiate(rangePrefab);
            _rangeIndicator.transform.localPosition = new Vector3(radius, radius, 1);
        }

        FollowMouse();
    }

    private IEnumerator DeactivateAbility(float delay, Vector2 mousePos)
    {
        _isActive = false; // 비활성화해서 이후 클릭을 방지
        SpriteRenderer sr = _rangeIndicator.GetComponent<SpriteRenderer>();
        sr.color = Color.red;

        yield return new WaitForSeconds(delay);

        DealDamage(mousePos);

        if (_rangeIndicator != null)
            Destroy(_rangeIndicator);
    }

    private void FollowMouse()
    {
        Vector3 mousePos = GetMousePosition();
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

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePos;
    }
}