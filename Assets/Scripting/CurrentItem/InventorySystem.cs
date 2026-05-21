using UnityEngine;

public enum SelectedArea
{
    None,
    Slot,
    Bag
}

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventoryCurrent inventoryCurrent;

    private SelectedArea selectedArea = SelectedArea.None;
    private int selectedIndex = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PressM();
        }
    }

    public void SelectItem(SelectedArea area, int index)
    {
        if (area == SelectedArea.Slot)
        {
            if (index < 0 || index >= inventoryCurrent.CurrentOriginalSlot.Length)
                return;

            if (inventoryCurrent.CurrentOriginalSlot[index] == null)
                return;
        }
        else if (area == SelectedArea.Bag)
        {
            if (!inventoryCurrent.IsBagOpen)
                return;

            if (index < 0 || index >= inventoryCurrent.CurrentBag.Length)
                return;

            if (inventoryCurrent.CurrentBag[index] == null)
                return;
        }
        else
        {
            return;
        }

        selectedArea = area;
        selectedIndex = index;
    }

    public void PressM()
    {
        if (selectedArea == SelectedArea.None || selectedIndex < 0)
            return;

        if (selectedArea == SelectedArea.Slot)
        {
            ItemStack movingStack = inventoryCurrent.CurrentOriginalSlot[selectedIndex];

            if (!CanMoveTo(inventoryCurrent.CurrentBag, movingStack))
            {
                Debug.Log("가방이 가득 차서 이동할 수 없습니다.");
                return;
            }

            MoveItem(inventoryCurrent.CurrentOriginalSlot, inventoryCurrent.CurrentBag, selectedIndex);

            if (IsFull(inventoryCurrent.CurrentBag))
            {
                inventoryCurrent.CloseBag();
            }
        }
        else if (selectedArea == SelectedArea.Bag)
        {
            ItemStack movingStack = inventoryCurrent.CurrentBag[selectedIndex];

            if (!CanMoveTo(inventoryCurrent.CurrentOriginalSlot, movingStack))
            {
                Debug.Log("기본 슬롯이 가득 차서 이동할 수 없습니다.");
                return;
            }

            MoveItem(inventoryCurrent.CurrentBag, inventoryCurrent.CurrentOriginalSlot, selectedIndex);
        }

        inventoryCurrent.SyncCurrentSlot();
        ClearSelection();
    }

    private void MoveItem(ItemStack[] fromArray, ItemStack[] toArray, int fromIndex)
    {
        if (fromIndex < 0 || fromIndex >= fromArray.Length)
            return;

        ItemStack movingStack = fromArray[fromIndex];

        if (movingStack == null)
            return;

        int remainingCount = movingStack.Count;

        for (int i = 0; i < toArray.Length; i++)
        {
            if (toArray[i] == null)
                continue;

            if (toArray[i].Item.Id != movingStack.Item.Id)
                continue;

            if (toArray[i].Count >= toArray[i].MaxStack)
                continue;

            int space = toArray[i].MaxStack - toArray[i].Count;
            int moveCount = Mathf.Min(space, remainingCount);

            toArray[i].Count += moveCount;
            remainingCount -= moveCount;

            if (remainingCount <= 0)
            {
                fromArray[fromIndex] = null;
                return;
            }
        }

        while (remainingCount > 0)
        {
            int emptyIndex = FindEmptyIndex(toArray);

            if (emptyIndex == -1)
            {
                movingStack.Count = remainingCount;
                Debug.Log("빈 칸이 부족해서 일부 아이템만 이동했습니다.");
                return;
            }

            int moveCount = Mathf.Min(movingStack.MaxStack, remainingCount);

            toArray[emptyIndex] = new ItemStack(
                movingStack.Item,
                moveCount,
                movingStack.Icon,
                movingStack.MaxStack
            );

            remainingCount -= moveCount;
        }

        fromArray[fromIndex] = null;
    }

    private bool CanMoveTo(ItemStack[] toArray, ItemStack movingStack)
    {
        if (movingStack == null)
            return false;

        for (int i = 0; i < toArray.Length; i++)
        {
            if (toArray[i] == null)
                return true;

            if (toArray[i].Item.Id == movingStack.Item.Id &&
                toArray[i].Count < toArray[i].MaxStack)
            {
                return true;
            }
        }

        return false;
    }

    private int FindEmptyIndex(ItemStack[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
                return i;
        }

        return -1;
    }

    private bool IsFull(ItemStack[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
                return false;
        }

        return true;
    }

    private void ClearSelection()
    {
        selectedArea = SelectedArea.None;
        selectedIndex = -1;
    }
    
}