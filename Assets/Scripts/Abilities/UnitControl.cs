using System.Collections;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
    public float controlTime;
    public bool isUnitControl;
    public float boostMultiplier;

    private void Awake()
    {
        isUnitControl = false;
    }

    public void ActivateUnitControl(Collider2D other)
    {
        UnitStats otherStats = other.GetComponent<UnitStats>();
        if (otherStats.isAlly == -1) return; // 적군이면 리턴

        if (!isUnitControl) return;

        // 상대 넥서스 방향으로 이동
        UnitMovement unitMovement = other.GetComponent<UnitMovement>();
        unitMovement.HeadToEnemyNexus();

        StartCoroutine(BoostUnitSpeed(unitMovement, boostMultiplier));
    }

    private IEnumerator BoostUnitSpeed(UnitMovement unitMovement, float mult)
    {
        float originalSpeed = unitMovement.rb.velocity.magnitude;
        unitMovement.rb.velocity *= mult;

        yield return new WaitForSeconds(controlTime);

        isUnitControl = false;
        Vector2 currentVec = unitMovement.rb.velocity.normalized;
        unitMovement.rb.velocity = currentVec * originalSpeed;
    }
}