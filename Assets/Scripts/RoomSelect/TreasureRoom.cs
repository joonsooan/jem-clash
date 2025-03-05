using UnityEngine;
using UnityEngine.UI;

public class TreasureRoom : MonoBehaviour
{
    public Button claimBtn;

    public void OnClick()
    {
        RoomManager.Instance.HideScreen();
    }
}