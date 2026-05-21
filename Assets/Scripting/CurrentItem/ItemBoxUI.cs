using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBoxUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Button button;

    private InventorySystem inventorySystem;
    private SelectedArea area;
    private int realIndex;

    public void Setup(InventorySystem inventory, SelectedArea selectedArea, int index, ItemStack stack)
    {
        inventorySystem = inventory;
        area = selectedArea;
        realIndex = index;

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
        }

        if (stack == null)
        {
            ClearVisual();
            return;
        }

        if (iconImage != null)
        {
            iconImage.sprite = stack.Icon;
            iconImage.enabled = stack.Icon != null;
        }

        if (countText != null)
            countText.text = stack.Count > 1 ? stack.Count.ToString() : "";

        if (button != null)
            button.interactable = true;
    }

    public void ClearVisual()
    {
        if (iconImage != null)
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
        }

        if (countText != null)
            countText.text = "";

        if (button != null)
            button.interactable = false;
    }

    private void OnClick()
    {
        if (inventorySystem == null)
            return;

        inventorySystem.SelectItem(area, realIndex);
    }
}