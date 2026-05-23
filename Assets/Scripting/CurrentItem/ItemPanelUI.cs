using UnityEngine;

public class ItemPanelUI : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private InventoryCurrent inventoryCurrent;
    [SerializeField] private ItemBoxUI[] itemBoxes;

    
    private const int PageSize = 10;
    private int currentPage = 0;

    private void Start()
    {
        if (inventoryCurrent != null)
            inventoryCurrent.SyncCurrentSlot();

        Refresh();
    }

    public void OnClickDownButton()
    {
        if (inventoryCurrent == null)
            return;

        currentPage++;

        int maxPage = (inventoryCurrent.SlotSize - 1) / PageSize;

        if (currentPage > maxPage)
            currentPage = maxPage;

        Refresh();
    }

    public void OnClickUpButton()
    {
        currentPage--;

        if (currentPage < 0)
            currentPage = 0;

        Refresh();
    }

    public void Refresh()
{
    Debug.Log("[ItemPanelUI] Refresh 호출됨", this);

    if (inventorySystem == null)
    {
        Debug.LogError("ItemPanelUI: inventorySystem이 연결되지 않았습니다.", this);
        return;
    }

    if (inventoryCurrent == null)
    {
        Debug.LogError("ItemPanelUI: inventoryCurrent가 연결되지 않았습니다.", this);
        return;
    }

    if (itemBoxes == null)
    {
        Debug.LogError("ItemPanelUI: itemBoxes 배열이 연결되지 않았습니다.", this);
        return;
    }

    inventoryCurrent.SyncCurrentSlot();

    int startIndex = currentPage * PageSize;

    for (int i = 0; i < itemBoxes.Length; i++)
    {
        if (itemBoxes[i] == null)
        {
            Debug.LogError($"ItemPanelUI: itemBoxes[{i}]가 비어 있습니다.", this);
            continue;
        }

        int realIndex = startIndex + i;

        if (realIndex >= inventoryCurrent.SlotSize)
        {
            Debug.Log(
                "[ItemPanelUI] 범위 밖 슬롯 비우기" +
                "\nBox Index: " + i +
                "\nReal Index: " + realIndex,
                itemBoxes[i]
            );

            itemBoxes[i].Setup(
                inventorySystem,
                SelectedArea.None,
                -1,
                null
            );

            continue;
        }

        ItemStack stack = null;

        if (inventoryCurrent.CurrentSlot != null &&
            realIndex >= 0 &&
            realIndex < inventoryCurrent.CurrentSlot.Length)
        {
            stack = inventoryCurrent.CurrentSlot[realIndex];
        }

        SelectedArea area;
        int areaIndex;

        if (realIndex < InventoryCurrent.OriginalSlotSize)
        {
            area = SelectedArea.Slot;
            areaIndex = realIndex;
        }
        else
        {
            area = SelectedArea.Bag;
            areaIndex = realIndex - InventoryCurrent.OriginalSlotSize;
        }

        Debug.Log(
            "[ItemPanelUI] Setup 호출 직전" +
            "\nBox Index: " + i +
            "\nReal Index: " + realIndex +
            "\nArea: " + area +
            "\nArea Index: " + areaIndex +
            "\nStack null?: " + (stack == null) +
            "\nItem null?: " + (stack != null && stack.Item == null) +
            "\nId: " + (stack != null && stack.Item != null ? stack.Item.Id : "NULL") +
            "\nName: " + (stack != null && stack.Item != null ? stack.Item.Name : "NULL") +
            "\nCount: " + (stack != null ? stack.Count.ToString() : "NULL"),
            itemBoxes[i]
        );

        itemBoxes[i].Setup(
            inventorySystem,
            area,
            areaIndex,
            stack
        );
    }
}
}