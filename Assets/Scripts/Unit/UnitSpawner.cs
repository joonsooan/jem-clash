using System.Collections;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Data")] public UnitData allyData;

    public UnitData enemyData;
    public Transform[] spawnPoints;

    [Header("Numbers")] public int spawnCount;

    public int unitCost;
    public float autoSpawnInterval;
    public bool isAutoSpawn;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        if (isAutoSpawn) StartCoroutine(SpawnUnitsCoroutine());
    }

    private void Update()
    {
        KeyTest();
    }

    public void SetAutoSpawn(bool value)
    {
        isAutoSpawn = value;

        if (isAutoSpawn)
            StartCoroutine(SpawnUnitsCoroutine());
        else
            StopCoroutine(SpawnUnitsCoroutine());
    }

    private IEnumerator SpawnUnitsCoroutine()
    {
        while (isAutoSpawn)
        {
            SpawnAllyUnit(spawnCount);
            yield return new WaitForSeconds(autoSpawnInterval);
        }
    }

    private void SpawnUnits()
    {
        GameObject allyUnit = GameManager.Instance.poolManager.Get(0);
        InitUnit(allyUnit, allyData, spawnPoints[1].position);

        GameObject enemyUnit = GameManager.Instance.poolManager.Get(1);
        InitUnit(enemyUnit, enemyData, spawnPoints[2].position);
    }

    public void SpawnAllyUnit(int count)
    {
        int maxCount = GameManager.Instance.resourceManager.supply / unitCost;

        if (maxCount <= 0)
            return;

        count = Mathf.Min(count, maxCount);

        for (int i = 0; i < count; i++)
        {
            GameObject allyUnit = GameManager.Instance.poolManager.Get(0);
            InitUnit(allyUnit, allyData, spawnPoints[1].position);
        }

        GameManager.Instance.resourceManager.SpendSupply(unitCost * count);
    }

    // 키보드 입력 테스트용
    private void KeyTest()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject allyUnit = GameManager.Instance.poolManager.Get(0);
            InitUnit(allyUnit, allyData, spawnPoints[1].position);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject enemyUnit = GameManager.Instance.poolManager.Get(1);
            InitUnit(enemyUnit, enemyData, spawnPoints[2].position);
        }
    }

    private void InitUnit(GameObject unit, UnitData data, Vector3 position)
    {
        unit.transform.position = position;
        UnitStats stats = unit.GetComponent<UnitStats>();
        stats.data = data;
        // Debug.Log($"Move Speed : {stats.data.moveSpeed}");
        stats.InitStats();
    }
}