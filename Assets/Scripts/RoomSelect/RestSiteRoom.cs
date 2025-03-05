using UnityEngine;
using UnityEngine.UI;

public class RestSiteRoom : MonoBehaviour
{
    public Image image;
    public Button restBtn;
    public Button gambleBtn;

    public void OnClick()
    {
        RoomManager.Instance.HideScreen();
    }
}
