using UnityEngine;
using Random = UnityEngine.Random;

public class Firework : MonoBehaviour
{
    public int unitCount;

    public Transform[] _spawnPoints = new Transform[3];
    public float yOffset;

    private void Awake()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
            _spawnPoints[i] = new GameObject($"SpawnPoint_{i}").transform;
    }

    public void SetFireworkPoints()
    {
        _spawnPoints[0].position = new Vector2(Random.Range(-9f, -7f), yOffset + Random.Range(-6f, 6f));
        _spawnPoints[1].position = new Vector2(Random.Range(-2f, 2f), yOffset + Random.Range(-6f, 6f));
        _spawnPoints[2].position = new Vector2(Random.Range(7f, 9f), yOffset + Random.Range(-6f, 6f));
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
            GameObject pooledObject = GameManager.instance.poolManager.Get(0);
            Transform spawnUnit = pooledObject.transform;
            spawnUnit.SetParent(spawnPoint);

            // 위치, 회전 초기화
            spawnUnit.localPosition = Vector3.zero;
            spawnUnit.localRotation = Quaternion.identity;

            // 원형으로 배치 후 간격 살짝 벌림
            Vector3 rotVec = Vector3.forward * 360 * i / unitCount;
            spawnUnit.Rotate(rotVec);
            spawnUnit.Translate(spawnUnit.up * 0.5f, Space.World);
            spawnUnit.Rotate(-rotVec);

            // 지정한 방향으로 이동 시작
            Vector2 dirVec = (spawnUnit.position - spawnPoint.position).normalized;
            UnitStats stats = spawnUnit.GetComponent<UnitStats>();
            spawnUnit.GetComponent<Rigidbody2D>().velocity = dirVec * stats.moveSpeed;

            spawnUnit.SetParent(GameManager.instance.poolManager.transform);
        }
    }
}