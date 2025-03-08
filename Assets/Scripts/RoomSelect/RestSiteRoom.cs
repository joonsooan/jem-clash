using UnityEngine;
using UnityEngine.UI;

public class RestSiteRoom : MonoBehaviour
{
    public CameraController cameraController;

    [Header("UI Elements")] public Image image;
    public Button restBtn;
    public Button gambleBtn;

    public void OnClick()
    {
        RoomManager.instance.HideScreen();
        cameraController.CameraShiftToRight();
    }
}