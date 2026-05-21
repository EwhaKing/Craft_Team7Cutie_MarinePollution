using UnityEngine;

public enum SelectedArea
{
    None,
    Slot,
    Bag
}

public class InventorySystem
{
    private const int OriginalSlotSize = 20;
    private const int MaxBagSize = 40;
    private const int MaxSlotSize = OriginalSlotSize + MaxBagSize;

    private bool Bag = false;

    // 현재 가방이 열려 있으면 40, 닫혀 있으면 0
    public int BagSize
    {
        get
        {
            return Bag ? MaxBagSize : 0;
        }
    }

    // 현재 화면에 보여줄 슬롯 수
    public int SlotSize
    {
        get
        {
            return OriginalSlotSize + BagSize;
        }
    }

    public ItemStack[] CurrentSlot = new ItemStack[MaxSlotSize];                 // 전체 표시용 슬롯 60칸
    public ItemStack[] CurrentOriginalSlot = new ItemStack[OriginalSlotSize];   // 기본 슬롯 20칸
    public ItemStack[] CurrentBag = new ItemStack[MaxBagSize];                  // 가방 슬롯 40칸

    private SelectedArea selectedArea = SelectedArea.None;
    private int selectedIndex = -1;

    public void SelectItem(SelectedArea area, int index)
    {
        if (area == SelectedArea.Slot)
        {
            if (index < 0 || index >= CurrentOriginalSlot.Length)
                return;

            if (CurrentOriginalSlot[index] == null)
                return;
        }
        else if (area == SelectedArea.Bag)
        {
            if (!Bag)
                return;

            if (index < 0 || index >= CurrentBag.Length)
                return;

            if (CurrentBag[index] == null)
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
            if (IsFull(CurrentBag))
            {
                Debug.Log("가방이 가득 차서 이동할 수 없습니다.");
                return;
            }

            MoveItem(CurrentOriginalSlot, CurrentBag, selectedIndex);

            if (IsFull(CurrentBag))
            {
                CloseBag();
            }
        }
        else if (selectedArea == SelectedArea.Bag)
        {
            if (IsFull(CurrentOriginalSlot))
            {
                Debug.Log("기본 슬롯이 가득 차서 이동할 수 없습니다.");
                return;
            }

            MoveItem(CurrentBag, CurrentOriginalSlot, selectedIndex);
        }

        SyncCurrentSlot();
        ClearSelection();
    }

    private void MoveItem(ItemStack[] fromArray, ItemStack[] toArray, int fromIndex)
    {
        if (fromIndex < 0 || fromIndex >= fromArray.Length) //
            return;

        ItemStack movingStack = fromArray[fromIndex];

        if (movingStack == null)
            return;

        int remainingCount = movingStack.Count;

        // 1. 같은 아이템 슬롯에 먼저 99개까지 채우기
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

        // 2. 남은 개수가 있으면 새 빈 슬롯에 최대 99개씩 배정
        while (remainingCount > 0)
        {
            int emptyIndex = FindEmptyIndex(toArray);

            if (emptyIndex == -1)
            {
                // 다 못 옮긴 경우 원래 슬롯에 남은 수량 유지
                movingStack.Count = remainingCount;
                Debug.Log("빈 칸이 부족해서 일부 아이템만 이동했습니다.");
                return;
            }

            int moveCount = Mathf.Min(movingStack.MaxStack, remainingCount);

            toArray[emptyIndex] = new ItemStack(
                movingStack.Item,
                moveCount,
                movingStack.MaxStack
            );

            remainingCount -= moveCount;
        }

        // 3. 전부 이동했으면 원래 슬롯 비우기
        fromArray[fromIndex] = null;
    }

    private void SyncCurrentSlot() //CurrentOriginalSlot + CurrentBag를 합쳐서 CurrentSlot에 반영하는 함수이다. 
    {
        // 전체 슬롯 초기화
        for (int i = 0; i < CurrentSlot.Length; i++)
        {
            CurrentSlot[i] = null;
        }

        // 기본 슬롯 0 ~ 19
        for (int i = 0; i < OriginalSlotSize; i++)
        {
            CurrentSlot[i] = CurrentOriginalSlot[i];
        }

        // 가방이 열려 있을 때만 20 ~ 59 표시
        if (Bag)
        {
            for (int i = 0; i < MaxBagSize; i++)
            {
                CurrentSlot[OriginalSlotSize + i] = CurrentBag[i];
            }
        }
    }

    private int FindSameItemIndex(ItemStack[] array, string itemId)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null && array[i].Item.Id == itemId)
                return i;
        }

        return -1;
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

    public void OpenBag()
    {
        Bag = true;
        SyncCurrentSlot();
    }

    public void CloseBag()
    {
        Bag = false;
        ClearSelection();
        SyncCurrentSlot();

        Debug.Log("가방이 닫혔습니다.");
    }

    public bool IsBagOpen()
    {
        return Bag;
    }

    private void ClearSelection()
    {
        selectedArea = SelectedArea.None;
        selectedIndex = -1;
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
}