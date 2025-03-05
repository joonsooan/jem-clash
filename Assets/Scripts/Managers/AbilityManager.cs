using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}