using System.Collections;
using UnityEngine;

public class Blover : MonoBehaviour
{
    public float blowMagnitude;

    public void ActivateBlover()
    {
        StartCoroutine(BlowEnemies());
    }

    private IEnumerator BlowEnemies()
    {
        for (int i = 0; i < 10; i++)
        {
            GameManager.Instance.poolManager.BlowEnemies(blowMagnitude);
            yield return new WaitForSeconds(0.1f);
        }
    }
}