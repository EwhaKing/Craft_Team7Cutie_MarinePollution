using UnityEngine;

public class AttachedEquippmentManager : MonoBehaviour
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public string[] allowedItemIds;
        public GameObject slotObject;
        public SpriteRenderer iconRenderer;
        public string equippedItemId;
    }

    [Header("장착 슬롯")]
    public EquipmentSlot[] equipmentSlots;

    [Header("인벤토리 연결")]
    public InventoryCurrent inventoryCurrent;

    [Header("가방 UI")]
    public GameObject bagPanel;

    [Header("가방 상태")]
    [SerializeField]
    private bool hasCollectingBag = false;

    public bool HasCollectingBag => hasCollectingBag;

    private void Awake()
    {
        ApplyBagState();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ApplyBagState();
    }
#endif

    public bool EquipItem(string itemId)
    {
        Debug.Log("[AttachedEquipment] EquipItem 호출됨: " + itemId, this);

        EquipmentSlot targetSlot = FindSlotByItemId(itemId);

        Debug.Log("[AttachedEquipment] targetSlot null?: " + (targetSlot == null), this);

        if (targetSlot == null)
        {
            Debug.LogWarning("이 아이템 ID는 장착할 수 없습니다: " + itemId, this);
            return false;
        }

        targetSlot.equippedItemId = itemId;

        if (targetSlot.slotObject != null)
        {
            targetSlot.slotObject.SetActive(true);
            Debug.Log("[AttachedEquipment] slotObject 활성화: " + targetSlot.slotObject.name, targetSlot.slotObject);
        }
        else
        {
            Debug.LogWarning("[AttachedEquipment] targetSlot.slotObject가 null입니다.", this);
        }

        Debug.Log("[AttachedEquipment] IsCollectingBagItem?: " + IsCollectingBagItem(itemId), this);

        if (IsCollectingBagItem(itemId))
        {
            hasCollectingBag = true;
            Debug.Log("[AttachedEquipment] hasCollectingBag = true", this);
            ApplyBagState();
        }

        Debug.Log(itemId + " 아이템을 장착했습니다.", this);
        return true;
    }

    public void UnequipItem(string itemId)
    {
        EquipmentSlot targetSlot = FindEquippedSlotByItemId(itemId);

        if (targetSlot == null)
        {
            Debug.LogWarning("현재 장착 중인 아이템이 아닙니다: " + itemId, this);
            return;
        }

        targetSlot.equippedItemId = "";

        if (targetSlot.iconRenderer != null)
        {
            targetSlot.iconRenderer.sprite = null;
        }

        if (targetSlot.slotObject != null)
        {
            targetSlot.slotObject.SetActive(false);
        }

        if (IsCollectingBagItem(itemId))
        {
            hasCollectingBag = false;
            ApplyBagState();
        }

        Debug.Log(itemId + " 아이템을 해제했습니다.", this);
    }

    private void ApplyBagState()
    {
        Debug.Log(
            "[AttachedEquipment] ApplyBagState 호출\n" +
            "hasCollectingBag: " + hasCollectingBag + "\n" +
            "bagPanel null?: " + (bagPanel == null) + "\n" +
            "bagPanel name: " + (bagPanel != null ? bagPanel.name : "NULL") + "\n" +
            "inventoryCurrent null?: " + (inventoryCurrent == null),
            this
        );

        if (bagPanel != null)
        {
            bagPanel.SetActive(hasCollectingBag);
            Debug.Log("[AttachedEquipment] bagPanel activeSelf: " + bagPanel.activeSelf, bagPanel);
        }

        if (inventoryCurrent == null)
        {
            return;
        }

        if (hasCollectingBag)
        {
            inventoryCurrent.OpenBag();
        }
        else
        {
            inventoryCurrent.CloseBag();
        }
    }

    private bool IsCollectingBagItem(string itemId)
    {
        return itemId == "106";
    }
    private EquipmentSlot FindSlotByItemId(string itemId)
    {
        foreach (EquipmentSlot slot in equipmentSlots)
        {
            foreach (string allowedId in slot.allowedItemIds)
            {
                if (allowedId == itemId)
                {
                    return slot;
                }
            }
        }

        return null;
    }

    private EquipmentSlot FindEquippedSlotByItemId(string itemId)
    {
        foreach (EquipmentSlot slot in equipmentSlots)
        {
            if (slot.equippedItemId == itemId)
            {
                return slot;
            }
        }

        return null;
    }
    
    public bool IsEquipped(string itemId)
    {
        foreach (EquipmentSlot slot in equipmentSlots)
        {
            if (slot.equippedItemId == itemId)
            {
                return true;
            }
        }

        return false;
    }

    public bool HasWetSuit()
    {
        return IsEquipped("107");
    }
}