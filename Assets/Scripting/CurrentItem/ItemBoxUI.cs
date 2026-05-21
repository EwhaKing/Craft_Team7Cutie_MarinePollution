using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBoxUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
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

        if (iconImage == null)
            iconImage = GetComponentInChildren<Image>();

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

        if (stack == null)
        {
            ClearVisual();
            return;
        }

        if (iconImage != null)
        {
            iconImage.sprite = stack.Icon;
            iconImage.enabled = stack.Icon != null;
            iconImage.preserveAspect = true;
        }

        if (countText != null)
        {
            countText.text = stack.Count > 1 ? stack.Count.ToString() : "";
            countText.enabled = stack.Count > 1;
        }
    }

    public void ClearVisual()
    {
        currentStack = null;

        if (iconImage != null)
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
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
    }
    
}