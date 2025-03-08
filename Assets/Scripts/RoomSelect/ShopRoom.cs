using UnityEngine;
using UnityEngine.UI;

public class ShopRoom : MonoBehaviour
{
    public CameraController cameraController;

    [Header("UI Elements")] public Button continueBtn;

    public void OnClick()
    {
        RoomManager.instance.HideScreen();
        cameraController.CameraShiftToRight();
    }
}