using UnityEngine;

public class PlayerItemPicker : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;

    private DroppedItem nearbyDroppedItem;

    private void Update()
    {
        if (nearbyDroppedItem == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    private void TryPickup()
    {
        ItemStack stack = nearbyDroppedItem.ItemStack;

        bool added = inventorySystem.AddItemStack(stack);

        if (added)
        {
            nearbyDroppedItem.Pickup();
            nearbyDroppedItem = null;
        }
        else
        {
            Debug.Log("인벤토리에 공간이 없습니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DroppedItem droppedItem = other.GetComponent<DroppedItem>();

        if (droppedItem == null)
            return;

        if (droppedItem.ItemStack == null)
            return;

        nearbyDroppedItem = droppedItem;
        Debug.Log($"{droppedItem.ItemStack.ItemName} 줍기 가능");
    }
    public bool TryPickup(ItemStack itemStack)
    {
        if (inventorySystem == null)
        {
            Debug.LogWarning("InventorySystem이 연결되지 않았습니다.", this);
            return false;
        }

        if (itemStack == null)
        {
            Debug.LogWarning("itemStack이 null입니다.", this);
            return false;
        }

        bool added = inventorySystem.AddItemStack(itemStack);

        Debug.Log("PlayerItemPicker AddItemStack 결과: " + added, this);

        return added;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DroppedItem droppedItem = other.GetComponent<DroppedItem>();

        if (droppedItem == nearbyDroppedItem)
        {
            nearbyDroppedItem = null;
        }
    }
    private void OnEnable()
    {
        Debug.Log("PlayerItemPicker 활성화됨");
    }
}