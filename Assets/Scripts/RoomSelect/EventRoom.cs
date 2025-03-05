using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventRoom : MonoBehaviour
{
    public CameraController cameraController;

    [Header("UI Elements")] public Image eventImage;
    public TMP_Text eventDescription;
    public Button btn1;
    public Button btn2;

    public void OnClick()
    {
        RoomManager.Instance.HideScreen();
        cameraController.CameraShiftToRight();
    }
}