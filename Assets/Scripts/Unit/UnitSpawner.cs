using System.Collections;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Data")] public UnitData allyData;

    public UnitData enemyData;

    [Header("Spawn Points")] public Transform[] spawnPoints;

    [Header("Numbers")] public int spawnCount;

    public int unitCost;
    public float autoSpawnInterval;
    public bool isAutoSpawn;

    private UnitDataBackup _allyDataBackup;
    private UnitDataBackup _enemyDataBackup;

    private void Awake()
    {
        // 유닛 기본 데이터 백업
        _allyDataBackup = new UnitDataBackup(allyData);
        _enemyDataBackup = new UnitDataBackup(enemyData);

        spawnPoints = GetComponentsInChildren<Transform>();
        if (isAutoSpawn) StartCoroutine(SpawnUnitsCoroutine());
    }

    private void Update()
    {
        KeyTest();
    }

    private void OnDestroy()
    {
        // 유닛 기본 데이터 복구
        _allyDataBackup.Restore(allyData);
        _enemyDataBackup.Restore(enemyData);
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

    // private void SpawnUnits()
    // {
    //     GameObject allyUnit = GameManager.Instance.poolManager.Get(0);
    //     SpawnUnit(allyUnit, allyData, spawnPoints[1].position);
    //
    //     GameObject enemyUnit = GameManager.Instance.poolManager.Get(1);
    //     SpawnUnit(enemyUnit, enemyData, spawnPoints[2].position);
    // }

    public void SpawnAllyUnit(int count)
    {
        int maxCount = GameManager.Instance.resourceManager.supply / unitCost;

        if (maxCount <= 0)
            return;

        count = Mathf.Min(count, maxCount);

        for (int i = 0; i < count; i++)
        {
            GameObject allyUnit = GameManager.Instance.poolManager.Get(0);
            SpawnUnit(allyUnit, allyData, spawnPoints[1].position);
        }

        GameManager.Instance.resourceManager.SpendSupply(unitCost * count);
    }

    // 키보드 입력 테스트용
    private void KeyTest()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject allyUnit = GameManager.Instance.poolManager.Get(0);
            SpawnUnit(allyUnit, allyData, spawnPoints[1].position);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject enemyUnit = GameManager.Instance.poolManager.Get(1);
            SpawnUnit(enemyUnit, enemyData, spawnPoints[2].position);
        }
    }

    private void SpawnUnit(GameObject unit, UnitData data, Vector3 position)
    {
        unit.transform.position = position;
        UnitStats stats = unit.GetComponent<UnitStats>();
        stats.unitData = data;
        // Debug.Log($"Move Speed : {stats.data.moveSpeed}");
        stats.InitStats();
    }
}