using UnityEngine;

public class ShopClick : MonoBehaviour
{
    public GameObject shopPanel;

    void Start()
    {
        shopPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        shopPanel.SetActive(true);
    }
}