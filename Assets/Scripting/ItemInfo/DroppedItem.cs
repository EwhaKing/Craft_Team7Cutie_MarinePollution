using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private string itemId;

    [Header("View")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    public string ItemId => itemId;

    private void Awake()
    {
        ApplyItemIcon();
    }

    private void ApplyItemIcon()
    {
        Item item = ItemDatabase.GetItemById(itemId);

        if (item == null)
        {
            Debug.LogError("[DroppedItem] Item을 찾을 수 없습니다. Id: " + itemId, this);
            return;
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("[DroppedItem] SpriteRenderer가 없습니다.", this);
            return;
        }

        spriteRenderer.sprite = item.Icon;

        Debug.Log("[DroppedItem] 아이콘 적용 완료: " + item.Name, this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("[DroppedItem] Trigger 감지: " + other.name, this);

        if (!other.CompareTag("Player"))
            return;

        PlayerItemPicker picker = other.GetComponent<PlayerItemPicker>();

        if (picker == null)
        {
            Debug.LogWarning("[DroppedItem] PlayerItemPicker가 없습니다.", other);
            return;
        }

        bool picked = picker.TryPickup(itemId);

        Debug.Log("[DroppedItem] TryPickup 결과: " + picked, this);

        if (picked)
        {
            Pickup();
        }
    }

    public void Init(string itemId)
    {
        this.itemId = itemId;
        ApplyItemIcon();
    }

    public void Pickup()
    {
        Destroy(gameObject);
    }
}