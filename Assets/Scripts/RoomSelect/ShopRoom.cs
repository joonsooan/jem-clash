using UnityEngine;
using UnityEngine.UI;

public class ShopRoom : MonoBehaviour
{
    public Button continueBtn;

    public void OnClick()
    {
        RoomManager.Instance.HideScreen();
    }
}
