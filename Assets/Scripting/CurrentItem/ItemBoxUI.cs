using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemBoxUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Button button;

    private InventorySystem inventory;
    private SelectedArea area;
    private int index;
    private ItemStack currentStack;

    private Canvas rootCanvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private bool isDragging;
    private Transform originalParent;
    private int originalSiblingIndex;
    private Vector2 originalPosition;

    public InventorySystem Inventory => inventory;
    public SelectedArea Area => area;
    public int Index => index;
    public ItemStack CurrentStack => currentStack;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rootCanvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

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

        itemImage.gameObject.SetActive(true);
        itemImage.sprite = item.Icon;
        itemImage.enabled = item.Icon != null;
        itemImage.preserveAspect = true;
        itemImage.color = Color.white;

        if (item.Icon != null)
        {
            itemImage.transform.localScale = new Vector3(3f, 3f, 1f);
        }
        else
        {
            itemImage.transform.localScale = Vector3.one;
        }
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

        if (!isDragging && canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        if (itemImage != null)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
            itemImage.transform.localScale = Vector3.one;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentStack == null || currentStack.Item == null || currentStack.Count <= 0)
        {
            isDragging = false;
            return;
        }

        isDragging = true;

        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();
        originalPosition = rectTransform.anchoredPosition;

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        Debug.Log("[ItemBoxUI] 드래그 시작: " + currentStack.Item.Id, this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
            return;

        if (rootCanvas == null)
            return;

        rectTransform.anchoredPosition += eventData.delta / rootCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool hadOriginalParent = originalParent != null;

        isDragging = false;

        if (hadOriginalParent)
        {
            transform.SetParent(originalParent, false);
            transform.SetSiblingIndex(originalSiblingIndex);
        }

        rectTransform.anchoredPosition = originalPosition;
        rectTransform.localScale = Vector3.one;
        rectTransform.localRotation = Quaternion.identity;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        Debug.Log("[ItemBoxUI] 드래그 종료 - 원래 위치 강제 복귀", this);
    }
}