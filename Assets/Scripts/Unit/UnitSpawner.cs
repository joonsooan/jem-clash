using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public UnitData allyData;
    public UnitData enemyData;
    public Transform[] spawnPoints;

    private int i;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        i = 0;
    }

    private void Update()
    {
        // 유닛 100개씩으로 시작
        if (i < 100)
        {
            GameObject allyUnit = GameManager.Instance.poolManager.Get(0);
            InitUnit(allyUnit, allyData, spawnPoints[1].position);
            GameObject enemyUnit = GameManager.Instance.poolManager.Get(1);
            InitUnit(enemyUnit, enemyData, spawnPoints[2].position);
            i++;
        }

        KeyTest();
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
        stats.InitStats();
    }
}