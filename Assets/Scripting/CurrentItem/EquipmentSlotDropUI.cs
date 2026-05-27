using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipmentSlotDropUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private AttachedEquippmentManager equipmentManager;

    [Header("이 장착 슬롯에 허용되는 아이템 ID")]
    [SerializeField] private string[] allowedItemIds;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("[EquipmentSlotDropUI] OnDrop 호출됨", this);

        if (eventData.pointerDrag == null)
        {
            Debug.LogWarning("[EquipmentSlotDropUI] pointerDrag가 null입니다.", this);
            return;
        }

        ItemBoxUI draggedSlot = eventData.pointerDrag.GetComponent<ItemBoxUI>();

        if (draggedSlot == null)
        {
            draggedSlot = eventData.pointerDrag.GetComponentInParent<ItemBoxUI>();
        }

        if (draggedSlot == null)
        {
            Debug.LogWarning("[EquipmentSlotDropUI] 드래그된 ItemBoxUI를 찾지 못했습니다.", this);
            return;
        }

        ItemStack stack = draggedSlot.CurrentStack;

        if (stack == null || stack.Item == null)
        {
            Debug.LogWarning("[EquipmentSlotDropUI] 드래그된 슬롯에 아이템이 없습니다.", this);
            return;
        }

        if (draggedSlot.Inventory == null)
        {
            Debug.LogWarning("[EquipmentSlotDropUI] draggedSlot.Inventory가 null입니다.", this);
            return;
        }

        if (draggedSlot.Area == SelectedArea.None || draggedSlot.Index < 0)
        {
            Debug.LogWarning("[EquipmentSlotDropUI] 유효하지 않은 인벤토리 슬롯입니다.", this);
            return;
        }

        string itemId = stack.Item.Id;

        Debug.Log("[EquipmentSlotDropUI] 드롭된 itemId: " + itemId, this);

        if (!IsAllowed(itemId))
        {
            Debug.LogWarning("[EquipmentSlotDropUI] 이 슬롯에는 장착할 수 없습니다. Id: " + itemId, this);
            return;
        }

        if (equipmentManager == null)
        {
            Debug.LogWarning("[EquipmentSlotDropUI] equipmentManager가 연결되지 않았습니다.", this);
            return;
        }

        bool equipped = equipmentManager.EquipItem(itemId);

        if (!equipped)
        {
            Debug.LogWarning("[EquipmentSlotDropUI] AttachedEquippmentManager에서 장착 실패: " + itemId, this);
            return;
        }

        draggedSlot.Inventory.RemoveStackAt(draggedSlot.Area, draggedSlot.Index);

        Debug.Log("[EquipmentSlotDropUI] 장착 성공 후 인벤토리에서 제거: " + itemId, this);
    }

    private bool IsAllowed(string itemId)
    {
        if (allowedItemIds == null)
            return false;

        for (int i = 0; i < allowedItemIds.Length; i++)
        {
            if (allowedItemIds[i] == itemId)
                return true;
        }

        return false;
    }
}