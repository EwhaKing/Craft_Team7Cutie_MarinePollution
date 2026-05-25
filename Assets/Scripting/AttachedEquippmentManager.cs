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

    public void EquipItem(string itemId)
    {
        EquipmentSlot targetSlot = FindSlotByItemId(itemId);

        if (targetSlot == null)
        {
            Debug.LogWarning("이 아이템 ID는 장착할 수 없습니다: " + itemId, this);
            return;
        }

        targetSlot.equippedItemId = itemId;

        if (targetSlot.slotObject != null)
        {
            targetSlot.slotObject.SetActive(true);
        }

        // 아이템 아이콘 교체 부분
        // string iconPath = itemData.iconPath;
        // Sprite iconSprite = Resources.Load<Sprite>(iconPath);
        // targetSlot.iconRenderer.sprite = iconSprite;

        if (IsCollectingBagItem(itemId))
        {
            hasCollectingBag = true;
            ApplyBagState();
        }

        Debug.Log(itemId + " 아이템을 장착했습니다.", this);
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
        if (bagPanel != null)
        {
            bagPanel.SetActive(hasCollectingBag);
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
        return itemId == "107_1" || itemId == "107_2" || itemId == "107";
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
}