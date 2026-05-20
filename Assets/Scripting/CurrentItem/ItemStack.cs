[System.Serializable]
public class ItemStack
{
    public string itemId;
    public int amount;
    public int maxStack;

    public ItemStack(string itemId, int amount, int maxStack = 64)
    {
        this.itemId = itemId;
        this.amount = amount;
        this.maxStack = maxStack;
    }
}