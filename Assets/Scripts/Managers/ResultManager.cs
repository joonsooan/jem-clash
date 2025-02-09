using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [Header("Game Objects")] public GameObject summaryPanel;

    public GameObject shopPanel;
    public GameObject darkEffect;

    private SceneChanger _sceneChanger;

    private void Awake()
    {
        _sceneChanger = GetComponent<SceneChanger>();
    }

    public void OpenSummaryPanel()
    {
        darkEffect.SetActive(true);
        summaryPanel.SetActive(true);
    }

    private void CloseSummaryPanel()
    {
        summaryPanel.SetActive(false);
    }

    public void OpenShopPanel()
    {
        CloseSummaryPanel();
        shopPanel.SetActive(true);
    }

    private void CloseShopPanel()
    {
        shopPanel.SetActive(false);
    }

    public void OpenGameOverPanel()
    {
        darkEffect.SetActive(true);
    }

    public void LoadMapSelect()
    {
        darkEffect.SetActive(false);
        _sceneChanger.LoadMapSelect();
    }
}