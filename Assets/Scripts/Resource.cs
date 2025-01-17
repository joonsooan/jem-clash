using System.Collections;
using UnityEngine;

public enum ResourceType
{
    Supply,
    Energy
}

public class Resource : MonoBehaviour
{
    public ResourceType resourceType;
    public int resourceInterval = 1;
    public int resourceAmount = 1;

    private bool _playerInZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = true;
            StartCoroutine(GainResource());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _playerInZone = false;
    }

    private IEnumerator GainResource()
    {
        while (_playerInZone)
        {
            yield return new WaitForSeconds(resourceInterval);

            switch (resourceType)
            {
                case ResourceType.Supply:
                    GameManager.Instance.resourceManager.AddSupply(resourceAmount);
                    break;
                case ResourceType.Energy:
                    GameManager.Instance.resourceManager.AddEnergy(resourceAmount);
                    break;
            }
        }
    }
}