using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBoxUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Button button;

    private InventorySystem inventory;
    private SelectedArea area;
    private int index;
    private ItemStack currentStack;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.RemoveListener(OnClick);
            button.onClick.AddListener(OnClick);
        }

        ClearVisual();
    }

    public void Setup(InventorySystem inventory, SelectedArea area, int index, ItemStack stack)
    {
        this.inventory = inventory;
        this.area = area;
        this.index = index;
        this.currentStack = stack;

        if (stack == null || stack.Item == null || stack.Count <= 0)
        {
            ClearVisual();
            return;
        }

        string itemId = stack.Item.Id;

        Item item = ItemDatabase.GetItemById(itemId);

        if (item == null)
        {
            Debug.LogWarning("[ItemBoxUI] Item을 찾을 수 없습니다. Id: " + itemId, this);
            ClearVisual();
            return;
        }

        ApplyItemImage(item);
        ApplyCount(stack.Count);

        Debug.Log(
            "[ItemBoxUI] 표시 완료 / " +
            "Id: " + item.Id +
            " / Name: " + item.Name +
            " / Count: " + stack.Count +
            " / Icon null?: " + (item.Icon == null),
            this
        );
    }

    private void ApplyItemImage(Item item)
    {
        if (itemImage == null)
        {
            Debug.LogWarning("[ItemBoxUI] itemImage가 연결되지 않았습니다.", this);
            return;
        }

        Debug.Log(
            "[ItemBoxUI] ApplyItemImage / " +
            "itemImage GameObject: " + itemImage.gameObject.name +
            " / sprite: " + (item.Icon == null ? "null" : item.Icon.name),
            this
        );

        itemImage.gameObject.SetActive(true);
        itemImage.sprite = item.Icon;
        itemImage.enabled = item.Icon != null;
        itemImage.preserveAspect = true;
        itemImage.color = Color.white;
    }

    private void ApplyCount(int count)
    {
        if (countText == null)
            return;

        countText.text = count > 1 ? count.ToString() : "";
        countText.enabled = count > 1;
    }

    public void ClearVisual()
    {
        currentStack = null;

        if (itemImage != null)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
        }

        if (countText != null)
        {
            countText.text = "";
            countText.enabled = false;
        }
    }

    private void OnClick()
    {
        Debug.Log($"클릭된 슬롯: {area}, {index}");

        if (inventory != null && area != SelectedArea.None && index >= 0)
        {
            inventory.SelectItem(area, index);
        }
    }
}