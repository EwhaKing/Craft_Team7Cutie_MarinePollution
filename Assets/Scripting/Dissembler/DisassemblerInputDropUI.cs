using UnityEngine;
using UnityEngine.EventSystems;

public class DisassemblerInputDropUI : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    [SerializeField] private DisassemblerSystem disassemblerSystem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("[DisassemblerInputDropUI] 마우스가 Input/Image 위에 들어옴", this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("[DisassemblerInputDropUI] OnDrop 호출됨", this);

        if (disassemblerSystem == null)
        {
            Debug.LogWarning("[DisassemblerInputDropUI] DisassemblerSystem이 연결되지 않았습니다.", this);
            return;
        }

        if (eventData.pointerDrag == null)
        {
            Debug.LogWarning("[DisassemblerInputDropUI] pointerDrag가 null입니다.", this);
            return;
        }

        ItemBoxUI draggedItemBox = eventData.pointerDrag.GetComponent<ItemBoxUI>();

        if (draggedItemBox == null)
            draggedItemBox = eventData.pointerDrag.GetComponentInParent<ItemBoxUI>();

        if (draggedItemBox == null)
        {
            Debug.LogWarning("[DisassemblerInputDropUI] ItemBoxUI를 찾지 못했습니다. Drag Object: " + eventData.pointerDrag.name, this);
            return;
        }

        ItemStack draggedStack = draggedItemBox.CurrentStack;

        if (draggedStack == null || draggedStack.Item == null || draggedStack.Count <= 0)
        {
            Debug.LogWarning("[DisassemblerInputDropUI] 드래그된 슬롯이 비어 있습니다.", this);
            return;
        }

        bool success = disassemblerSystem.TrySetInput(draggedStack);

        Debug.Log(
            "[DisassemblerInputDropUI] TrySetInput 결과: " + success +
            " / Item Id: " + draggedStack.Item.Id +
            " / Item Name: " + draggedStack.Item.Name +
            " / Item Type: " + draggedStack.Item.GetType().Name,
            this
        );

        if (!success)
            return;

        RemoveOneFromInventory(draggedItemBox);

        Debug.Log("[DisassemblerInputDropUI] Input 등록 완료: " + draggedStack.Item.Name, this);
    }

    private void RemoveOneFromInventory(ItemBoxUI itemBox)
    {
        InventorySystem inventory = itemBox.Inventory;

        if (inventory == null)
        {
            Debug.LogWarning("[DisassemblerInputDropUI] InventorySystem이 없습니다.", this);
            return;
        }

        ItemStack stack = inventory.GetStack(itemBox.Area, itemBox.Index);

        if (stack == null || stack.Item == null)
            return;

        stack.Count -= 1;

        if (stack.Count <= 0)
            inventory.RemoveStackAt(itemBox.Area, itemBox.Index);
        else
            inventory.RefreshUI();
    }
}