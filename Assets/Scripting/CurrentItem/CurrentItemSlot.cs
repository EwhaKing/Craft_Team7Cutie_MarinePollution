using UnityEngine;

public static class CurrentItemSlot
{
    public static int MaxSlotCount = 20;
    public static ItemStack[] slots = new ItemStack[MaxSlotCount];

    public static ItemStack GetSlot(int index)
    {
        if (!IsValidIndex(index))
            return null;

        return slots[index];
    }

    public static void SetSlot(int index, ItemStack stack)
    {
        if (!IsValidIndex(index))
            return;

        slots[index] = stack;
    }

    public static void ClearSlot(int index)
    {
        
        
        if (!IsValidIndex(index))
            return;

        slots[index] = null;
    }

    public static bool IsValidIndex(int index)
    {
        return index >= 0 && index < slots.Length;
    }

    public static void MoveOrMergeSlot(int fromIndex, int toIndex)
    {
        if (!IsValidIndex(fromIndex) || !IsValidIndex(toIndex))
            return;

        if (fromIndex == toIndex)
            return;

        ItemStack from = slots[fromIndex];
        ItemStack to = slots[toIndex];

        if (from == null)
            return;

        if (to == null)
        {
            slots[toIndex] = from;
            slots[fromIndex] = null;
            return;
        }

        if (from.itemId == to.itemId)
        {
            int availableSpace = to.maxStack - to.amount;

            if (availableSpace <= 0)
                return;

            int moveAmount = Mathf.Min(availableSpace, from.amount);

            to.amount += moveAmount;
            from.amount -= moveAmount;

            if (from.amount <= 0)
                slots[fromIndex] = null;

            return;
        }

        slots[fromIndex] = to;
        slots[toIndex] = from;
    }

}