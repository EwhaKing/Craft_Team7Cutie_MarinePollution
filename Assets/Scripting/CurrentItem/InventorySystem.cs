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
    private void Awake()
    {
        if (inventoryCurrent == null)
        {
            inventoryCurrent = GetComponent<InventoryCurrent>();
        }

        if (inventoryCurrent == null)
        {
            inventoryCurrent = FindFirstObjectByType<InventoryCurrent>();
        }

        if (inventoryCurrent == null)
        {
            Debug.LogError("InventoryCurrent를 찾지 못했습니다. InventoryCurrent 컴포넌트를 씬에 추가하거나 InventorySystem에 연결하세요.", this);
        }
        else
        {
            Debug.Log("InventoryCurrent 연결됨: " + inventoryCurrent.name, this);
        }
    }
    private SelectedArea selectedArea = SelectedArea.None;
    private int selectedIndex = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PressM();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryCurrent.PrintInventory();
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
    
    
    private bool AddStackToArray(ItemStack[] targetArray, ItemStack incomingStack)
    {
        if (targetArray == null || incomingStack == null)
            return false;

        int remainingCount = incomingStack.Count;

        for (int i = 0; i < targetArray.Length; i++)
        {
            if (targetArray[i] == null)
                continue;

            if (targetArray[i].Item.Id != incomingStack.Item.Id)
                continue;

            if (targetArray[i].Count >= targetArray[i].MaxStack)
                continue;

            int space = targetArray[i].MaxStack - targetArray[i].Count;
            int addCount = Mathf.Min(space, remainingCount);

            targetArray[i].Count += addCount;
            remainingCount -= addCount;

            if (remainingCount <= 0)
            {
                incomingStack.Count = 0;
                return true;
            }
        }

        while (remainingCount > 0)
        {
            int emptyIndex = FindEmptyIndex(targetArray);

            if (emptyIndex == -1)
            {
                incomingStack.Count = remainingCount;
                return false;
            }

            int addCount = Mathf.Min(incomingStack.MaxStack, remainingCount);

            targetArray[emptyIndex] = new ItemStack(
                incomingStack.Item,
                addCount,
                incomingStack.Icon,
                incomingStack.MaxStack
            );

            remainingCount -= addCount;
        }

        incomingStack.Count = 0;
        return true;
    }
    public bool AddItemStack(ItemStack incomingStack)
    {
        if (inventoryCurrent == null)
        {
            Debug.LogError("AddItemStack 실패: inventoryCurrent가 null입니다.", this);
            return false;
        }

        if (incomingStack == null)
        {
            Debug.LogWarning("AddItemStack 실패: incomingStack이 null입니다.", this);
            return false;
        }

        if (incomingStack.Item == null)
        {
            Debug.LogWarning("AddItemStack 실패: incomingStack.Item이 null입니다.", this);
            return false;
        }

        if (incomingStack.Count <= 0)
        {
            Debug.LogWarning("AddItemStack 실패: Count가 0 이하입니다.", this);
            return false;
        }

        ItemStack stackToAdd = new ItemStack(
            incomingStack.Item,
            incomingStack.Count,
            incomingStack.Icon,
            incomingStack.MaxStack
        );

        AddStackToArray(inventoryCurrent.CurrentOriginalSlot, stackToAdd);

        if (stackToAdd.Count > 0)
        {
            AddStackToArray(inventoryCurrent.CurrentBag, stackToAdd);
        }

        bool addedAll = stackToAdd.Count <= 0;

        if (addedAll)
        {
            inventoryCurrent.SyncCurrentSlot();
            Debug.Log("아이템 저장 성공", this);
            inventoryCurrent.PrintInventory();
        }
        else
        {
            Debug.LogWarning("아이템 저장 실패: 공간 부족", this);
        }

        return addedAll;
    }
}