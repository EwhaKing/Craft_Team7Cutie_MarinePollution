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
        Refresh();
    }

    public void OnClickDownButton()
    {
        currentPage++;

        int maxPage = (inventoryCurrent.CurrentOriginalSlot.Length - 1) / PageSize;

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
        int startIndex = currentPage * PageSize;

        for (int i = 0; i < itemBoxes.Length; i++)
        {
            int realIndex = startIndex + i;

            ItemStack stack = null;

            if (realIndex < inventoryCurrent.CurrentOriginalSlot.Length)
            {
                stack = inventoryCurrent.CurrentOriginalSlot[realIndex];
            }

            itemBoxes[i].Setup(
                inventorySystem,
                SelectedArea.Slot,
                realIndex,
                stack
            );
        }
    }
}