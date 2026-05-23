using UnityEngine;

public enum SelectedArea
{
    None,
    Slot,
    Bag
}

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory Data")]
    [SerializeField] private InventoryCurrent inventoryCurrent;

    [Header("Slot UI")]
    [SerializeField] private ItemBoxUI[] originalSlotUIs;

    [Header("Bag UI")]
    [SerializeField] private ItemBoxUI[] bagSlotUIs;

    [SerializeField] private ItemPanelUI itemPanelUI;
    
    private SelectedArea selectedArea = SelectedArea.None;
    private int selectedIndex = -1;

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

    private void Start()
    {
        RefreshUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PressM();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryCurrent == null)
            {
                Debug.LogError("InventoryCurrent가 없어서 출력할 수 없습니다.", this);
                return;
            }

            inventoryCurrent.PrintInventory();
        }
    }

    public bool AddItemStack(ItemStack incomingStack)
    {
        Debug.Log(
            "[InventorySystem] AddItemStack 진입\n" +
            "incomingStack null?: " + (incomingStack == null) + "\n" +
            "Item null?: " + (incomingStack != null && incomingStack.Item == null) + "\n" +
            "Id: " + (incomingStack != null && incomingStack.Item != null ? incomingStack.Item.Id : "NULL_ITEM") + "\n" +
            "Name: " + (incomingStack != null && incomingStack.Item != null ? incomingStack.Item.Name : "NULL_ITEM") + "\n" +
            "Count: " + (incomingStack != null ? incomingStack.Count.ToString() : "NULL") + "\n" +
            "Icon null?: " + (incomingStack != null && incomingStack.Icon == null) + "\n" +
            "Icon name: " + (incomingStack != null && incomingStack.Icon != null ? incomingStack.Icon.name : "NULL"),
            this
        );
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

        Debug.Log(
            "[AddItemStack] 추가 시작\n" +
            "Id: " + stackToAdd.Item.Id + "\n" +
            "Name: " + stackToAdd.Item.Name + "\n" +
            "Count: " + stackToAdd.Count + "\n" +
            "MaxStack: " + stackToAdd.MaxStack + "\n" +
            "Icon null?: " + (stackToAdd.Icon == null) + "\n" +
            "Icon name: " + (stackToAdd.Icon != null ? stackToAdd.Icon.name : "NULL"),
            this
        );

        AddStackToArray(inventoryCurrent.CurrentOriginalSlot, stackToAdd);

        if (stackToAdd.Count > 0)
        {
            AddStackToArray(inventoryCurrent.CurrentBag, stackToAdd);
        }

        bool addedAll = stackToAdd.Count <= 0;

        Debug.Log(
            "[AddItemStack] 추가 결과\n" +
            "addedAll: " + addedAll + "\n" +
            "남은 Count: " + stackToAdd.Count,
            this
        );

        if (!addedAll)
        {
            Debug.LogWarning("아이템 저장 실패: 공간 부족", this);
            return false;
        }

        inventoryCurrent.SyncCurrentSlot();

        Debug.Log("아이템 저장 성공", this);
        inventoryCurrent.PrintInventory();

        RefreshUI();

        return true;
    }
    public bool AddItemById(string itemId)
    {
        if (string.IsNullOrEmpty(itemId))
        {
            Debug.LogWarning("[InventorySystem] AddItemById 실패: itemId가 비어 있습니다.", this);
            return false;
        }

        Item item = ItemDatabase.GetItemById(itemId);

        if (item == null)
        {
            Debug.LogWarning("[InventorySystem] AddItemById 실패: Item을 찾을 수 없습니다. Id: " + itemId, this);
            return false;
        }

        ItemStack stack = new ItemStack(
            item,
            1,
            item.Icon,
            99
        );

        return AddItemStack(stack);
    }
    private bool AddStackToArray(ItemStack[] targetArray, ItemStack incomingStack)
    {
        Debug.Log("========== [AddStackToArray] 시작 ==========");

        if (targetArray == null)
        {
            Debug.LogError("[AddStackToArray] targetArray가 null입니다.");
            return false;
        }

        if (incomingStack == null)
        {
            Debug.LogError("[AddStackToArray] incomingStack이 null입니다.");
            return false;
        }

        if (incomingStack.Item == null)
        {
            Debug.LogError("[AddStackToArray] incomingStack.Item이 null입니다.");
            return false;
        }

        if (incomingStack.Count <= 0)
        {
            Debug.Log("[AddStackToArray] incomingStack.Count가 이미 0 이하입니다.");
            return true;
        }

        Debug.Log(
            "[AddStackToArray] 들어온 아이템 확인\n" +
            "Id: " + incomingStack.Item.Id + "\n" +
            "Name: " + incomingStack.Item.Name + "\n" +
            "Count: " + incomingStack.Count + "\n" +
            "MaxStack: " + incomingStack.MaxStack + "\n" +
            "Icon null?: " + (incomingStack.Icon == null) + "\n" +
            "Icon name: " + (incomingStack.Icon != null ? incomingStack.Icon.name : "NULL")
        );

        // 1. 같은 아이템이 있으면 먼저 합치기
        for (int i = 0; i < targetArray.Length; i++)
        {
            ItemStack currentStack = targetArray[i];

            if (IsEmptyStack(currentStack))
            {
                continue;
            }

            if (currentStack.Item.Id != incomingStack.Item.Id)
            {
                continue;
            }

            if (currentStack.Count >= currentStack.MaxStack)
            {
                continue;
            }

            int space = currentStack.MaxStack - currentStack.Count;
            int addAmount = Mathf.Min(space, incomingStack.Count);

            currentStack.Count += addAmount;
            incomingStack.Count -= addAmount;

            Debug.Log(
                "[AddStackToArray] 같은 아이템 합침\n" +
                "Slot: " + i + "\n" +
                "AddAmount: " + addAmount + "\n" +
                "Current Count: " + currentStack.Count + "\n" +
                "Incoming Left: " + incomingStack.Count
            );

            if (incomingStack.Count <= 0)
            {
                return true;
            }
        }

        // 2. 빈 슬롯에 새 ItemStack으로 넣기
        for (int i = 0; i < targetArray.Length; i++)
        {
            ItemStack currentStack = targetArray[i];

            Debug.Log(
                "[AddStackToArray] 빈 슬롯 검사 " + i + "\n" +
                "Stack null?: " + (currentStack == null) + "\n" +
                "Item null?: " + (currentStack != null && currentStack.Item == null) + "\n" +
                "Count: " + (currentStack != null ? currentStack.Count.ToString() : "NULL") + "\n" +
                "IsEmpty?: " + IsEmptyStack(currentStack)
            );

            if (!IsEmptyStack(currentStack))
            {
                continue;
            }

            int moveCount = Mathf.Min(incomingStack.MaxStack, incomingStack.Count);

            targetArray[i] = new ItemStack(
                incomingStack.Item,
                moveCount,
                incomingStack.Icon,
                incomingStack.MaxStack
            );

            incomingStack.Count -= moveCount;

            Debug.Log(
                "[AddStackToArray] 빈 슬롯에 저장 완료\n" +
                "Slot: " + i + "\n" +
                "Id: " + targetArray[i].Item.Id + "\n" +
                "Name: " + targetArray[i].Item.Name + "\n" +
                "Saved Count: " + targetArray[i].Count + "\n" +
                "Incoming Left: " + incomingStack.Count + "\n" +
                "Icon null?: " + (targetArray[i].Icon == null) + "\n" +
                "Icon name: " + (targetArray[i].Icon != null ? targetArray[i].Icon.name : "NULL")
            );

            if (incomingStack.Count <= 0)
            {
                return true;
            }
        }

        Debug.LogWarning("[AddStackToArray] 저장 실패: 빈 슬롯이 없습니다.");
        return false;
    }
    

    private void RefreshSlotArray(ItemBoxUI[] slotUIs, ItemStack[] stacks, SelectedArea area)
    {
        if (slotUIs == null)
        {
            Debug.LogWarning("[InventorySystem] " + area + " slotUIs 배열이 null입니다.");
            return;
        }

        for (int i = 0; i < slotUIs.Length; i++)
        {
            ItemBoxUI slotUI = slotUIs[i];

            if (slotUI == null)
            {
                Debug.LogWarning("[InventorySystem] " + area + " UI " + i + "번 ItemBoxUI가 null입니다.");
                continue;
            }

            ItemStack stack = null;

            if (stacks != null && i < stacks.Length)
            {
                stack = stacks[i];
            }

            if (IsEmptyStack(stack))
            {
                stack = null;
            }

            Debug.Log(
                "[InventorySystem] ItemBoxUI.Setup 호출 직전\n" +
                "Area: " + area + "\n" +
                "Index: " + i + "\n" +
                "UI Object: " + slotUI.gameObject.name + "\n" +
                "Stack null?: " + (stack == null) + "\n" +
                "Id: " + (stack != null && stack.Item != null ? stack.Item.Id : "NULL") + "\n" +
                "Name: " + (stack != null && stack.Item != null ? stack.Item.Name : "NULL") + "\n" +
                "Count: " + (stack != null ? stack.Count.ToString() : "NULL") + "\n" +
                "Icon null?: " + (stack != null && stack.Icon == null) + "\n" +
                "Icon name: " + (stack != null && stack.Icon != null ? stack.Icon.name : "NULL"),
                slotUI
            );

            slotUI.Setup(this, area, i, stack);
        }
    }

    public void SelectItem(SelectedArea area, int index)
    {
        if (inventoryCurrent == null)
        {
            Debug.LogWarning("[InventorySystem] SelectItem 실패: inventoryCurrent가 null입니다.", this);
            return;
        }

        if (area == SelectedArea.Slot)
        {
            if (inventoryCurrent.CurrentOriginalSlot == null)
                return;

            if (index < 0 || index >= inventoryCurrent.CurrentOriginalSlot.Length)
                return;

            if (IsEmptyStack(inventoryCurrent.CurrentOriginalSlot[index]))
                return;
        }
        else if (area == SelectedArea.Bag)
        {
            if (!inventoryCurrent.IsBagOpen)
                return;

            if (inventoryCurrent.CurrentBag == null)
                return;

            if (index < 0 || index >= inventoryCurrent.CurrentBag.Length)
                return;

            if (IsEmptyStack(inventoryCurrent.CurrentBag[index]))
                return;
        }
        else
        {
            return;
        }

        selectedArea = area;
        selectedIndex = index;

        Debug.Log("[InventorySystem] 선택됨: " + selectedArea + " / " + selectedIndex, this);
    }

    public void PressM()
    {
        if (inventoryCurrent == null)
        {
            Debug.LogWarning("[InventorySystem] PressM 실패: inventoryCurrent가 null입니다.", this);
            return;
        }

        if (selectedArea == SelectedArea.None || selectedIndex < 0)
        {
            Debug.Log("[InventorySystem] 이동할 아이템이 선택되지 않았습니다.", this);
            return;
        }

        if (selectedArea == SelectedArea.Slot)
        {
            if (inventoryCurrent.CurrentOriginalSlot == null || inventoryCurrent.CurrentBag == null)
            {
                Debug.LogWarning("[InventorySystem] 슬롯 또는 가방 배열이 null입니다.", this);
                return;
            }

            if (selectedIndex >= inventoryCurrent.CurrentOriginalSlot.Length)
                return;

            ItemStack movingStack = inventoryCurrent.CurrentOriginalSlot[selectedIndex];

            if (IsEmptyStack(movingStack))
                return;

            if (!CanMoveTo(inventoryCurrent.CurrentBag, movingStack))
            {
                Debug.Log("가방이 가득 차서 이동할 수 없습니다.", this);
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
            if (inventoryCurrent.CurrentOriginalSlot == null || inventoryCurrent.CurrentBag == null)
            {
                Debug.LogWarning("[InventorySystem] 슬롯 또는 가방 배열이 null입니다.", this);
                return;
            }

            if (selectedIndex >= inventoryCurrent.CurrentBag.Length)
                return;

            ItemStack movingStack = inventoryCurrent.CurrentBag[selectedIndex];

            if (IsEmptyStack(movingStack))
                return;

            if (!CanMoveTo(inventoryCurrent.CurrentOriginalSlot, movingStack))
            {
                Debug.Log("기본 슬롯이 가득 차서 이동할 수 없습니다.", this);
                return;
            }

            MoveItem(inventoryCurrent.CurrentBag, inventoryCurrent.CurrentOriginalSlot, selectedIndex);
        }

        inventoryCurrent.SyncCurrentSlot();
        ClearSelection();
        RefreshUI();
    }

    private void MoveItem(ItemStack[] fromArray, ItemStack[] toArray, int fromIndex)
    {
        if (fromArray == null || toArray == null)
            return;

        if (fromIndex < 0 || fromIndex >= fromArray.Length)
            return;

        ItemStack movingStack = fromArray[fromIndex];

        if (IsEmptyStack(movingStack))
            return;

        int remainingCount = movingStack.Count;

        // 1. 같은 아이템이 있으면 합치기
        for (int i = 0; i < toArray.Length; i++)
        {
            ItemStack targetStack = toArray[i];

            if (IsEmptyStack(targetStack))
                continue;

            if (targetStack.Item.Id != movingStack.Item.Id)
                continue;

            if (targetStack.Count >= targetStack.MaxStack)
                continue;

            int space = targetStack.MaxStack - targetStack.Count;
            int moveCount = Mathf.Min(space, remainingCount);

            targetStack.Count += moveCount;
            remainingCount -= moveCount;

            if (remainingCount <= 0)
            {
                fromArray[fromIndex] = null;
                return;
            }
        }

        // 2. 빈 슬롯에 새로 넣기
        while (remainingCount > 0)
        {
            int emptyIndex = FindEmptyIndex(toArray);

            if (emptyIndex == -1)
            {
                movingStack.Count = remainingCount;
                Debug.Log("빈 칸이 부족해서 일부 아이템만 이동했습니다.", this);
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
        if (toArray == null)
            return false;

        if (IsEmptyStack(movingStack))
            return false;

        for (int i = 0; i < toArray.Length; i++)
        {
            ItemStack targetStack = toArray[i];

            if (IsEmptyStack(targetStack))
                return true;

            if (targetStack.Item.Id == movingStack.Item.Id &&
                targetStack.Count < targetStack.MaxStack)
            {
                return true;
            }
        }

        return false;
    }

    private int FindEmptyIndex(ItemStack[] array)
    {
        if (array == null)
            return -1;

        for (int i = 0; i < array.Length; i++)
        {
            if (IsEmptyStack(array[i]))
                return i;
        }

        return -1;
    }

    private bool IsFull(ItemStack[] array)
    {
        if (array == null)
            return true;

        for (int i = 0; i < array.Length; i++)
        {
            if (IsEmptyStack(array[i]))
                return false;
        }

        return true;
    }

    private bool IsEmptyStack(ItemStack stack)
    {
        return stack == null || stack.Item == null || stack.Count <= 0;
    }

    private void ClearSelection()
    {
        selectedArea = SelectedArea.None;
        selectedIndex = -1;
    }
    public void RefreshUI()
    {
        if (inventoryCurrent == null)
        {
            Debug.LogWarning("RefreshUI 실패: inventoryCurrent가 null입니다.", this);
            return;
        }

        inventoryCurrent.SyncCurrentSlot();

        Debug.Log("[InventorySystem] RefreshUI 호출됨", this);

        Debug.Log("[InventorySystem] originalSlotUIs null?: " + (originalSlotUIs == null), this);
        Debug.Log("[InventorySystem] originalSlotUIs Length: " + (originalSlotUIs != null ? originalSlotUIs.Length : -1), this);

        RefreshSlotArray(
            originalSlotUIs,
            inventoryCurrent.CurrentOriginalSlot,
            SelectedArea.Slot
        );

        Debug.Log("[InventorySystem] bagSlotUIs null?: " + (bagSlotUIs == null), this);
        Debug.Log("[InventorySystem] bagSlotUIs Length: " + (bagSlotUIs != null ? bagSlotUIs.Length : -1), this);

        RefreshSlotArray(
            bagSlotUIs,
            inventoryCurrent.CurrentBag,
            SelectedArea.Bag
        );

        if (itemPanelUI != null)
        {
            Debug.Log("[InventorySystem] itemPanelUI.Refresh 호출", itemPanelUI);
            itemPanelUI.Refresh();
        }
        else
        {
            Debug.LogWarning("[InventorySystem] itemPanelUI가 연결되지 않았습니다.", this);
        }
    }
}