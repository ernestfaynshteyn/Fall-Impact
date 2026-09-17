using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    public List<ShopItemSO> stock = new List<ShopItemSO>();
    private HashSet<string> purchasedUnlocks = new HashSet<string>();

    public event Action<ShopItemSO> OnPurchaseSuccess;
    public event Action<ShopItemSO> OnPurchaseFailed;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public bool CanAfford(ShopItemSO item, PlayerStats player) => player.Currency >= item.cost;

    public bool Purchase(ShopItemSO item, PlayerStats player)
    {
        if (item.type == ShopItemType.Unlock && purchasedUnlocks.Contains(item.itemId))
        {
            OnPurchaseFailed?.Invoke(item); // already owned
            return false;
        }

        if (!CanAfford(item, player))
        {
            OnPurchaseFailed?.Invoke(item);
            return false;
        }

        player.SpendCurrency(item.cost); // add this method to PlayerStats if missing
        ApplyItemEffect(item, player);

        if (item.type == ShopItemType.Unlock)
            purchasedUnlocks.Add(item.itemId);

        OnPurchaseSuccess?.Invoke(item);
        return true;
    }

    private void ApplyItemEffect(ShopItemSO item, PlayerStats player)
    {
        switch (item.type)
        {
            case ShopItemType.StatUpgrade:
                player.ModifyStat(item.statId, item.statAmount); // route into your existing Upgrades/SkillTree pipeline
                break;
            case ShopItemType.Consumable:
                InventoryManager.Instance?.AddItem(item.effectId, 1);
                break;
            case ShopItemType.Unlock:
                UnlockManager.Instance?.Unlock(item.effectId); // or whatever tracks unlocks in your game
                break;
        }
    }

    public bool IsUnlocked(string itemId) => purchasedUnlocks.Contains(itemId);
}
