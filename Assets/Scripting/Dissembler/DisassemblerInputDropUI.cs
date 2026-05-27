using UnityEngine;
using UnityEngine.EventSystems;

public class DisassemblerInputDropUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private DisassemblerSystem disassemblerSystem;

    public void OnDrop(PointerEventData eventData)
    {
        if (disassemblerSystem == null)
        {
            Debug.LogWarning("[DisassemblerInputDropUI] DisassemblerSystem이 연결되지 않았습니다.", this);
            return;
        }

        ItemBoxUI draggedItemBox = eventData.pointerDrag != null
            ? eventData.pointerDrag.GetComponent<ItemBoxUI>()
            : null;

        if (draggedItemBox == null)
        {
            Debug.LogWarning("[DisassemblerInputDropUI] 드래그된 오브젝트에서 ItemBoxUI를 찾지 못했습니다.", this);
            return;
        }

        ItemStack draggedStack = draggedItemBox.CurrentStack;

        if (draggedStack == null || draggedStack.Item == null || draggedStack.Count <= 0)
        {
            Debug.LogWarning("[DisassemblerInputDropUI] 드래그된 아이템이 비어 있습니다.", this);
            return;
        }

        bool success = disassemblerSystem.TrySetInput(draggedStack);

        if (!success)
            return;

        Debug.Log(
            "[DisassemblerInputDropUI] 분해기 input 등록: " +
            draggedStack.Item.Name +
            " x" + draggedStack.Count,
            this
        );
    }
}