using UnityEngine;

public class PlayerItemPicker : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;

    public bool TryPickup(string itemId)
    {
        if (inventorySystem == null)
        {
            Debug.LogWarning("[PlayerItemPicker] InventorySystem이 연결되지 않았습니다.", this);
            return false;
        }

        if (string.IsNullOrEmpty(itemId))
        {
            Debug.LogWarning("[PlayerItemPicker] itemId가 비어 있습니다.", this);
            return false;
        }

        bool added = inventorySystem.AddItemById(itemId);

        Debug.Log("[PlayerItemPicker] AddItemById 결과: " + added, this);

        return added;
    }

    private void OnEnable()
    {
        Debug.Log("PlayerItemPicker 활성화됨");
    }
}