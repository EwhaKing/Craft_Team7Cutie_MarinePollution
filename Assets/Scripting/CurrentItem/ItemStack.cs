public class ItemStack
{
    public Item Item { get; private set; }
    public int Count { get; set; }
    public int MaxStack { get; private set; }

    public string ItemId => Item.Id;
    public string ItemName => Item.Name;

    public ItemStack(Item item, int count, int maxStack = 99)
    {
        Item = item;
        Count = count;
        MaxStack = maxStack;
    }
}