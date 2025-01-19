using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public UnitData allyData;
    public UnitData enemyData;
    public Transform[] spawnPoints;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        // StartCoroutine(SpawnUnitsCoroutine());
    }

    private void Update()
    {
        KeyTest();
    }

    // private IEnumerator SpawnUnitsCoroutine()
    // {
    //     while (true)
    //     {
    //         for (int i = 0; i < 10; i++)
    //             SpawnUnits();
    //         yield return new WaitForSeconds(3f);
    //     }
    // }

    private void SpawnUnits()
    {
        GameObject allyUnit = GameManager.Instance.poolManager.Get(0);
        InitUnit(allyUnit, allyData, spawnPoints[1].position);

        GameObject enemyUnit = GameManager.Instance.poolManager.Get(1);
        InitUnit(enemyUnit, enemyData, spawnPoints[2].position);
    }

    public void SpawnAllyUnit()
    {
        GameObject allyUnit = GameManager.Instance.poolManager.Get(0);
        InitUnit(allyUnit, allyData, spawnPoints[1].position);
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
        Debug.Log($"Move Speed : {stats.data.moveSpeed}");
        stats.InitStats();
    }
}