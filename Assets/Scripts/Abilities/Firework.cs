using UnityEngine;
using Random = UnityEngine.Random;

public class Firework : MonoBehaviour
{
    public int unitCount;

    private readonly Transform[] _spawnPoints = new Transform[3];

    private void Awake()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
            _spawnPoints[i] = new GameObject($"SpawnPoint_{i}").transform;
    }

    public void SetFireworkPoints()
    {
        _spawnPoints[0].position = new Vector2(Random.Range(-5f, -3f), Random.Range(-3f, 3f));
        _spawnPoints[1].position = new Vector2(Random.Range(-1f, 1f), Random.Range(-3f, 3f));
        _spawnPoints[2].position = new Vector2(Random.Range(3f, 5f), Random.Range(-3f, 3f));
    }

    public void SpawnFireworks()
    {
        foreach (Transform spawnPoint in _spawnPoints)
            SpawnFirework(spawnPoint);
    }

    private void SpawnFirework(Transform spawnPoint)
    {
        for (int i = 0; i < unitCount; i++)
        {
            // 풀매니저에서 가져와서 부모 오브젝트 할당
            GameObject pooledObject = GameManager.Instance.poolManager.Get(0);
            Transform spawnUnit = pooledObject.transform;
            spawnUnit.parent = spawnPoint;

            // 위치, 회전 초기화
            spawnUnit.localPosition = Vector3.zero;
            spawnUnit.localRotation = Quaternion.identity;

            // 원형으로 배치 후 간격 살짝 벌림
            Vector3 rotVec = Vector3.forward * 360 * i / unitCount;
            spawnUnit.Rotate(rotVec);
            spawnUnit.Translate(spawnUnit.up * 0.2f, Space.World);

            // 지정한 방향으로 이동 시작
            Vector2 dirVec = (spawnPoint.position - spawnUnit.position).normalized;
            UnitStats stats = spawnUnit.GetComponent<UnitStats>();
            spawnUnit.GetComponent<Rigidbody2D>().velocity = dirVec * stats.moveSpeed;
        }
    }
}