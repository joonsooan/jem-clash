using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public ItemShopUI itemShopUI;
    public Button refreshButton;

    private void Start()
    {
        refreshButton.onClick.AddListener(() => itemShopUI.RerollShop());
    }
}