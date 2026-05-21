using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public Item Item { get; private set; }
    public int Count { get; set; }
    public int MaxStack { get; private set; }

    public Sprite Icon { get; private set; }

    public string Id
    {
        get
        {
            return Item != null ? Item.Id : "NULL_ITEM";
        }
    }

    public string Name
    {
        get
        {
            return Item != null ? Item.Name : "NULL_ITEM";
        }
    }

    public ItemStack(Item item, int count, Sprite icon, int maxStack)
    {
        Item = item;
        Count = count;
        Icon = icon;
        MaxStack = maxStack;
    }
}