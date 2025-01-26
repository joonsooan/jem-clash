using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject rangePrefab;
    public LayerMask targetLayer;
    public float radius;
    public int damageAmount;

    private bool _isActive;
    private GameObject _rangeIndicator;

    private void Update()
    {
        if (_isActive && _rangeIndicator != null) FollowMouse();

        if (_isActive && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = GetMousePosition();
            DealDamage(mousePos);
            DeactivateAbility();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_isActive)
        {
            Gizmos.color = Color.red;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Gizmos.DrawWireSphere(new Vector3(mousePos.x, mousePos.y, 0), radius);
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

    private void DeactivateAbility()
    {
        _isActive = false;

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