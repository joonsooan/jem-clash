using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Screens")]
    public GameObject eventScreen;
    public GameObject restSiteScreen;
    public GameObject shopScreen;
    public GameObject treasureScreen;
    
    private GameObject _currentScreen;
    private bool _isScreenActive = false;
    
    private void Update()
    {
        RoomSelect();
        // HideScreen();
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
                    case "Room_ToughEnemy":
                    case "Room_Boss":
                        break;
                }
            }
        }
    }

    void ShowEventScreen()
    {
        if (_currentScreen != null)
            _currentScreen.SetActive(false);
        eventScreen.SetActive(true);
        _currentScreen = eventScreen;
        
    }

    void ShowRestSiteScreen()
    {
        if (_currentScreen != null)
            _currentScreen.SetActive(false);
        restSiteScreen.SetActive(true);
        _currentScreen = restSiteScreen;
        
    }

    void ShowShopScreen()
    {
        if (_currentScreen != null)
            _currentScreen.SetActive(false);
        shopScreen.SetActive(true);
        _currentScreen = shopScreen;
        
    }

    void ShowTreasureScreen()
    {
        if (_currentScreen != null)
            _currentScreen.SetActive(false);
        treasureScreen.SetActive(true);
        _currentScreen = treasureScreen;
        
    }

    void HideScreen()
    {
        if (Input.GetMouseButtonDown(0) && _isScreenActive)
        {
            _currentScreen.SetActive(false);
            _isScreenActive = false;
        }
    }
}
