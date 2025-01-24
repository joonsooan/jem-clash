using UnityEngine;

public class UnitControl : MonoBehaviour
{
    public float controlTime;

    private void Start()
    {
        controlTime = GameManager.Instance.abilityManager.GetComponent<UnitControl>().controlTime;
    }
}