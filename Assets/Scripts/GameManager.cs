using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Managers")] public PoolManager poolManager;

    public ResourceManager resourceManager;
    public UnitSpawner unitSpawner;
    public GameObject abilityManager;

    [Header("Game Objects")] public bool gameLive;

    public GameObject player;
    public Transform playerNexus;
    public Transform enemyNexus;
    public GameObject supplyText;
    public GameObject energyText;

    private void Awake()
    {
        Instance = this;
        gameLive = true;
        abilityManager.GetComponent<UnitControl>().controlTime = 5f;
    }

    private void Update()
    {
        GameSpeedControl();
    }

    private void GameSpeedControl()
    {
        if (Input.GetKey(KeyCode.F1)) Time.timeScale = 1;
        else if (Input.GetKey(KeyCode.F2)) Time.timeScale = 2;
        else if (Input.GetKey(KeyCode.F3)) Time.timeScale = 3;
        else if (Input.GetKey(KeyCode.F4)) Time.timeScale = 4;
        else if (Input.GetKey(KeyCode.F5)) Time.timeScale = 0.2f;
    }

    public void Stop()
    {
        gameLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        gameLive = true;
        Time.timeScale = 1;
    }
}