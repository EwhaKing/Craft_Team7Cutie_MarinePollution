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

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        if (stack == null)
        {
            iconImage.sprite = null;
            iconImage.enabled = false;

            if (countText != null)
                countText.text = "";

            button.interactable = false;
            return;
        }

        iconImage.sprite = stack.Icon;
        iconImage.enabled = true;

        if (countText != null)
            countText.text = stack.Count > 1 ? stack.Count.ToString() : "";

        button.interactable = true;
    }

    private void OnClick()
    {
        inventorySystem.SelectItem(area, realIndex);
    }
}