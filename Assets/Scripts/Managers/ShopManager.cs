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

    // TODO: 화면 내 다른 곳을 클릭하면 현재 선택한 버튼의 설명 제거
}