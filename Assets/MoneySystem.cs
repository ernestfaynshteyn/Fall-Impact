using UnityEngine;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    public static int money = 0;

    public TextMeshProUGUI moneyText;

    void Update()
    {
        moneyText.text = "$ " + money;
    }
}