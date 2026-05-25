using UnityEngine;

public class InventoryFullPanelUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private InventoryCurrent inventoryCurrent;

    [Header("Original Slots")]
    [SerializeField] private ItemBoxUI[] originalSlotUIs;

    [Header("Bag Slots")]
    [SerializeField] private ItemBoxUI[] bagSlotUIs;

    [Header("Bag Parent")]
    [SerializeField] private GameObject bagSlotParent;

    private void Awake()
    {
        if (inventorySystem == null)
            inventorySystem = FindFirstObjectByType<InventorySystem>();

        if (inventoryCurrent == null)
            inventoryCurrent = FindFirstObjectByType<InventoryCurrent>();
    }

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (inventorySystem == null)
        {
            Debug.LogError("[InventoryFullPanelUI] inventorySystem이 없습니다.", this);
            return;
        }

        if (inventoryCurrent == null)
        {
            Debug.LogError("[InventoryFullPanelUI] inventoryCurrent가 없습니다.", this);
            return;
        }

        inventoryCurrent.SyncCurrentSlot();

        RefreshOriginalSlots();
        RefreshBagSlots();
    }

    private void RefreshOriginalSlots()
    {
        if (originalSlotUIs == null)
        {
            Debug.LogError("[InventoryFullPanelUI] originalSlotUIs가 연결되지 않았습니다.", this);
            return;
        }

        for (int i = 0; i < originalSlotUIs.Length; i++)
        {
            if (originalSlotUIs[i] == null)
            {
                Debug.LogError("[InventoryFullPanelUI] originalSlotUIs[" + i + "]가 비어 있습니다.", this);
                continue;
            }

            ItemStack stack = null;

            if (inventoryCurrent.CurrentOriginalSlot != null &&
                i < inventoryCurrent.CurrentOriginalSlot.Length)
            {
                stack = inventoryCurrent.CurrentOriginalSlot[i];
            }

            if (IsEmptyStack(stack))
                stack = null;

            Debug.Log(
                "[InventoryFullPanelUI] OriginalSlot 반영" +
                "\nIndex: " + i +
                "\nUI Object: " + originalSlotUIs[i].gameObject.name +
                "\nStack null?: " + (stack == null) +
                "\nItem Name: " + (stack != null && stack.Item != null ? stack.Item.Name : "NULL") +
                "\nCount: " + (stack != null ? stack.Count.ToString() : "NULL"),
                originalSlotUIs[i]
            );

            originalSlotUIs[i].Setup(
                inventorySystem,
                SelectedArea.Slot,
                i,
                stack
            );
        }
    }

    private void RefreshBagSlots()
    {
        bool bagOpen = inventoryCurrent.IsBagOpen;

        if (bagSlotParent != null)
            bagSlotParent.SetActive(bagOpen);

        if (bagSlotUIs == null)
        {
            Debug.LogError("[InventoryFullPanelUI] bagSlotUIs가 연결되지 않았습니다.", this);
            return;
        }

        if (!bagOpen)
            return;

        for (int i = 0; i < bagSlotUIs.Length; i++)
        {
            if (bagSlotUIs[i] == null)
            {
                Debug.LogError("[InventoryFullPanelUI] bagSlotUIs[" + i + "]가 비어 있습니다.", this);
                continue;
            }

            ItemStack stack = null;

            if (inventoryCurrent.CurrentBag != null &&
                i < inventoryCurrent.CurrentBag.Length)
            {
                stack = inventoryCurrent.CurrentBag[i];
            }

            if (IsEmptyStack(stack))
                stack = null;

            bagSlotUIs[i].Setup(
                inventorySystem,
                SelectedArea.Bag,
                i,
                stack
            );
        }
    }

    private bool IsEmptyStack(ItemStack stack)
    {
        return stack == null || stack.Item == null || stack.Count <= 0;
    }
}