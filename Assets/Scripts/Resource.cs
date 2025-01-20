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
    public int resourceAmount = 2;

    private bool _playerInZone;
    private Coroutine _resourceCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = true;
            if (_resourceCoroutine == null)
                _resourceCoroutine = StartCoroutine(GainResource());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = false;
            if (_resourceCoroutine != null)
            {
                StopCoroutine(_resourceCoroutine);
                _resourceCoroutine = null;
            }
        }
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