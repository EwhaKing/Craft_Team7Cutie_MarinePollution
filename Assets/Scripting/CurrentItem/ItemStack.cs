using UnityEngine;

public class ItemStack
{
    public Item Item { get; private set; }
    public int Count { get; set; }
    public int MaxStack { get; private set; }
    public Sprite Icon { get; private set; }

    public string ItemId => Item.Id;
    public string ItemName => Item.Name;

    public ItemStack(Item item, int count, Sprite icon = null, int maxStack = 99)
    {
        Item = item;
        Count = count;
        Icon = icon;
        MaxStack = maxStack;
    }
}