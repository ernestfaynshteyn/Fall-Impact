using UnityEngine;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    public int money = 0;

    public static MoneySystem Instance { get; private set; }

    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Self-destruct if a duplicate is found
            return;
        }

        Instance = this;

        // Optional: Keep this object alive across different scenes
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        moneyText.text = "$ " + money;
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }

        return false;
    }
}