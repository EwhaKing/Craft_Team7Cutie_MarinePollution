using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisassemblerOutputImageUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text countText;

    private DisassemblerSystem disassemblerSystem;
    private Item item;
    private int count;

    public Item Item => item;
    public int Count => count;

    private void Awake()
    {
        if (icon == null)
            icon = GetComponent<Image>();
    }

    public void Init(DisassemblerSystem system)
    {
        disassemblerSystem = system;
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;

        if (item == null || count <= 0)
        {
            Clear();
            return;
        }

        if (icon != null)
        {
            icon.sprite = item.Icon;
            icon.enabled = item.Icon != null;
            icon.preserveAspect = true;
            icon.color = Color.white;
        }

        if (countText != null)
        {
            countText.text = count > 1 ? count.ToString() : "";
            countText.enabled = count > 1;
        }

        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null || count <= 0)
            return;

        if (disassemblerSystem != null)
        {
            disassemblerSystem.CollectOutputSlot(this);
        }
    }

    public void Clear()
    {
        item = null;
        count = 0;

        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }

        if (countText != null)
        {
            countText.text = "";
            countText.enabled = false;
        }

        gameObject.SetActive(false);
    }
}