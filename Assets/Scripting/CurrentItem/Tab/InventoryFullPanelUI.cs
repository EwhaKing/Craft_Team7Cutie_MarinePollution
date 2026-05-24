using System.Collections.Generic;
using UnityEngine;

public class InventoryFullPanelUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private InventoryCurrent inventoryCurrent;

    [Header("Slot Parents")]
    [SerializeField] private Transform originSlotParent;
    [SerializeField] private Transform bagSlotParent;

    private readonly List<ItemBoxUI> originSlotUIs = new List<ItemBoxUI>();
    private readonly List<ItemBoxUI> bagSlotUIs = new List<ItemBoxUI>();

    private void Awake()
    {
        if (inventorySystem == null)
            inventorySystem = FindFirstObjectByType<InventorySystem>();

        if (inventoryCurrent == null)
            inventoryCurrent = FindFirstObjectByType<InventoryCurrent>();

        CollectSlots();
    }

    private void OnEnable()
    {
        CollectSlots();
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

    private void CollectSlots()
    {
        originSlotUIs.Clear();
        bagSlotUIs.Clear();

        CollectChildSlots(originSlotParent, originSlotUIs, "OriginalSlot");
        CollectChildSlots(bagSlotParent, bagSlotUIs, "BagSlot");

        Debug.Log(
            "[InventoryFullPanelUI] 슬롯 자동 등록 완료" +
            "\nOriginal Slot Count: " + originSlotUIs.Count +
            "\nBag Slot Count: " + bagSlotUIs.Count,
            this
        );
    }

    private void CollectChildSlots(
        Transform parent,
        List<ItemBoxUI> list,
        string parentName
    )
    {
        if (parent == null)
        {
            Debug.LogError("[InventoryFullPanelUI] " + parentName + " Parent가 연결되지 않았습니다.", this);
            return;
        }

        ItemBoxUI[] slots = parent.GetComponentsInChildren<ItemBoxUI>(true);

        for (int i = 0; i < slots.Length; i++)
        {
            list.Add(slots[i]);
        }
    }

    private void RefreshOriginalSlots()
    {
        int count = Mathf.Min(originSlotUIs.Count, InventoryCurrent.OriginalSlotSize);

        for (int i = 0; i < count; i++)
        {
            ItemStack stack = null;

            if (inventoryCurrent.CurrentOriginalSlot != null &&
                i < inventoryCurrent.CurrentOriginalSlot.Length)
            {
                stack = inventoryCurrent.CurrentOriginalSlot[i];
            }

            if (IsEmptyStack(stack))
                stack = null;

            originSlotUIs[i].Setup(
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
            bagSlotParent.gameObject.SetActive(bagOpen);

        if (!bagOpen)
            return;

        int count = Mathf.Min(bagSlotUIs.Count, InventoryCurrent.MaxBagSize);

        for (int i = 0; i < count; i++)
        {
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