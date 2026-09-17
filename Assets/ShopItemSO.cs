using UnityEngine;

public enum ShopItemType { StatUpgrade, Consumable, Unlock }

[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/Item")]
public class ShopItemSO : ScriptableObject
{
    public string itemId;
    public string itemName;
    [TextArea] public string description;
    public Sprite icon;
    public int cost;
    public ShopItemType type;

    // Used when type == StatUpgrade - statId should match whatever your
    // PlayerStats/Upgrades system already uses to identify a stat
    public string statId;
    public float statAmount;

    // Used when type == Consumable / Unlock
    public string effectId;
}
