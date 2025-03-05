using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    [Header("Screens")] public GameObject eventScreen;

    public GameObject restSiteScreen;
    public GameObject shopScreen;
    public GameObject treasureScreen;

    private GameObject _currentScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        RoomSelect();
    }

    private void RoomSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.tag);
                switch (hit.collider.gameObject.tag)
                {
                    case "Room_Event":
                        ShowEventScreen();
                        break;
                    case "Room_RestSite":
                        ShowRestSiteScreen();
                        break;
                    case "Room_Shop":
                        ShowShopScreen();
                        break;
                    case "Room_Treasure":
                        ShowTreasureScreen();
                        break;

                    case "Room_Enemy":
                        GoToGameScene();
                        break;
                    case "Room_ToughEnemy":
                    case "Room_Boss":
                        break;
                }
            }
        }
    }

    private void ShowEventScreen()
    {
        PrevScreenDeactivate();
        eventScreen.SetActive(true);
        _currentScreen = eventScreen;
    }

    private void ShowRestSiteScreen()
    {
        PrevScreenDeactivate();
        restSiteScreen.SetActive(true);
        _currentScreen = restSiteScreen;
    }

    private void ShowShopScreen()
    {
        PrevScreenDeactivate();
        shopScreen.SetActive(true);
        _currentScreen = shopScreen;
    }

    private void ShowTreasureScreen()
    {
        PrevScreenDeactivate();
        treasureScreen.SetActive(true);
        _currentScreen = treasureScreen;
    }

    private void GoToGameScene()
    {
        SceneChanger.Instance.LoadGame();
    }

    private void PrevScreenDeactivate()
    {
        if (_currentScreen != null)
            _currentScreen.SetActive(false);
    }

    public void HideScreen()
    {
        _currentScreen.SetActive(false);
    }
}