using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Managers")] public PoolManager poolManager;

    public ResourceManager resourceManager;
    public ResultManager resultManager;
    public UnitSpawner unitSpawner;
    public AbilityManager abilityManager;

    [Header("Game Objects")] public bool gameLive;

    public GameObject player;
    public Transform playerNexus;
    public Transform enemyNexus;
    public GameObject supplyText;
    public GameObject energyText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameLive = true;
    }

    private void Update()
    {
        GameSpeedControl();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        resourceManager = ResourceManager.instance;
        resultManager = ResultManager.instance;
        abilityManager = AbilityManager.instance;
    }

    private void GameSpeedControl()
    {
        if (Input.GetKey(KeyCode.F1)) Time.timeScale = 1;
        else if (Input.GetKey(KeyCode.F2)) Time.timeScale = 2;
        else if (Input.GetKey(KeyCode.F3)) Time.timeScale = 3;
        else if (Input.GetKey(KeyCode.F4)) Time.timeScale = 4;
        else if (Input.GetKey(KeyCode.F5)) Time.timeScale = 0.2f;
    }

    public void GameLose()
    {
        Stop();
        resultManager.OpenGameOverPanel();
    }

    public void GameWin()
    {
        Stop();
        resultManager.OpenSummaryPanel();
    }

    private void Stop()
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