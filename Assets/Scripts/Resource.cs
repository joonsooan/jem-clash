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
            yield return new WaitForSeconds(GameManager.Instance.resourceManager.resourceInterval);

            switch (resourceType)
            {
                case ResourceType.Supply:
                    GameManager.Instance.resourceManager.AddSupply(GameManager.Instance.resourceManager.supplyAmount);
                    break;
                case ResourceType.Energy:
                    GameManager.Instance.resourceManager.AddEnergy(GameManager.Instance.resourceManager.energyAmount);
                    break;
            }
        }
    }

    public void ResourceAmountUp(int amount)
    {
        switch (resourceType)
        {
            case ResourceType.Supply:
                GameManager.Instance.resourceManager.supplyAmount += amount;
                break;
            case ResourceType.Energy:
                GameManager.Instance.resourceManager.energyAmount += amount;
                break;
        }
    }
}