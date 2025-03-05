using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventRoom : MonoBehaviour
{
    public Image eventImage;
    public TMP_Text eventDescription;
    public Button btn1;
    public Button btn2;

    public void OnClick()
    {
        RoomManager.Instance.HideScreen();
    }
}
