using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private const int EnemyIndex = 1;

    public GameObject[] prefabs;
    private List<GameObject>[] _pools;

    private void Awake()
    {
        _pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < _pools.Length; i++)
            _pools[i] = new List<GameObject>();
    }

    public GameObject Get(int index) // 아군: 0, 적군: 1
    {
        GameObject select = null;

        foreach (GameObject item in _pools[index])
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }

        if (!select)
        {
            select = Instantiate(prefabs[index]);
            _pools[index].Add(select);
        }

        select.transform.SetParent(transform); // PoolManager의 자식으로 설정
        return select;
    }

    public void BlowEnemies(float magnitude)
    {
        foreach (GameObject enemy in _pools[EnemyIndex])
            if (enemy.activeSelf)
            {
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(magnitude * 0.1f, 0), ForceMode2D.Impulse);
            }
    }
}