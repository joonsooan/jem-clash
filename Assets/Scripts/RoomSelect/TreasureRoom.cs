using UnityEngine;
using UnityEngine.UI;

public class TreasureRoom : MonoBehaviour
{
    public CameraController cameraController;

    [Header("UI Elements")] public Button claimBtn;

    public void OnClick()
    {
        RoomManager.instance.HideScreen();
        cameraController.CameraShiftToRight();
    }
}