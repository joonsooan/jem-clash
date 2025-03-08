using System.Collections;
using UnityEngine;

public class Blover : MonoBehaviour
{
    public float blowMagnitude;

    public void ActivateAbility()
    {
        StartCoroutine(BlowEnemies());
    }

    private IEnumerator BlowEnemies()
    {
        for (int i = 0; i < 10; i++)
        {
            GameManager.instance.poolManager.BlowEnemies(blowMagnitude);
            yield return new WaitForSeconds(0.1f);
        }
    }
}