using UnityEngine;

public class COINS : MonoBehaviour
{
    public int coinValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoneySystem.Instance.money += coinValue;
            Destroy(gameObject);
        }
    }
}