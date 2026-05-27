using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisassemblerSystem : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private InventorySystem inventorySystem;

    [Header("Dropped Item")]
    [SerializeField] private DroppedItem droppedItemPrefab;
    [SerializeField] private Transform dropPoint;

    [Header("Input UI")]
    [SerializeField] private Image inputIcon;
    [SerializeField] private TMP_Text inputNameText;
    [SerializeField] private TMP_Text inputCountText;

    [Header("Output UI")]
    [SerializeField] private Transform outputParent;
    [SerializeField] private DisassemblerOutputImageUI outputSlotTemplate;

    private ItemStack inputStack;

    private bool hasCollectedAnyOutput;
    private bool isQuitting;

    private readonly List<DisassemblerOutputImageUI> outputSlots = new List<DisassemblerOutputImageUI>();

    private void Awake()
    {
        if (inventorySystem == null)
            inventorySystem = FindFirstObjectByType<InventorySystem>();

        if (outputSlotTemplate != null)
        {
            outputSlotTemplate.Init(this);
            outputSlotTemplate.Clear();
        }
    }

    private void Start()
    {
        ClearInputAndOutputs();
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDisable()
    {
        if (isQuitting)
            return;

        ResolveWhenPanelClosed();
    }

    public bool TrySetInput(ItemStack stack)
    {
        if (stack == null || stack.Item == null)
        {
            Debug.LogWarning("[DisassemblerSystem] 입력 아이템이 null입니다.", this);
            return false;
        }

        if (stack.Item is not TrashItem trashItem)
        {
            Debug.LogWarning("[DisassemblerSystem] TrashItem만 분해할 수 있습니다.", this);
            return false;
        }

        inputStack = new ItemStack(
            stack.Item,
            1,
            stack.Item.Icon,
            stack.MaxStack
        );

        hasCollectedAnyOutput = false;

        RefreshInputUI();
        ClearOutputs();
        RefreshOutputPreview(trashItem);

        return true;
    }

    private void RefreshInputUI()
    {
        if (inputStack == null || inputStack.Item == null)
            return;

        if (inputIcon != null)
        {
            inputIcon.sprite = inputStack.Item.Icon;
            inputIcon.enabled = inputStack.Item.Icon != null;
            inputIcon.preserveAspect = true;
            inputIcon.color = Color.white;
        }

        if (inputNameText != null)
            inputNameText.text = inputStack.Item.Name;

        if (inputCountText != null)
        {
            inputCountText.text = inputStack.Count > 1 ? inputStack.Count.ToString() : "";
            inputCountText.enabled = inputStack.Count > 1;
        }
    }

    private void RefreshOutputPreview(TrashItem trashItem)
    {
        if (trashItem.DecomposeResult == null || trashItem.DecomposeResult.Count <= 0)
            return;

        foreach (KeyValuePair<string, int> result in trashItem.DecomposeResult)
        {
            string resultItemId = result.Key;
            int resultCount = result.Value;

            Item resultItem = ItemDatabase.GetItemById(resultItemId);

            if (resultItem == null)
            {
                Debug.LogWarning("[DisassemblerSystem] 결과 아이템을 찾지 못했습니다. Id: " + resultItemId, this);
                continue;
            }

            AddOutputSlot(resultItem, resultCount);
        }
    }

    private void AddOutputSlot(Item item, int count)
    {
        if (outputSlotTemplate == null || outputParent == null)
        {
            Debug.LogWarning("[DisassemblerSystem] Output 설정이 비어 있습니다.", this);
            return;
        }

        DisassemblerOutputImageUI newSlot = Instantiate(outputSlotTemplate, outputParent);
        newSlot.Init(this);
        newSlot.Set(item, count);
        outputSlots.Add(newSlot);
    }

    public void CollectOutputSlot(DisassemblerOutputImageUI slot)
    {
        if (slot == null || slot.Item == null || slot.Count <= 0)
            return;

        hasCollectedAnyOutput = true;

        AddToInventoryOrDrop(slot.Item, slot.Count);

        outputSlots.Remove(slot);
        Destroy(slot.gameObject);

        ClearInputOnly();
    }

    private void ResolveWhenPanelClosed()
    {
        bool hasInput = inputStack != null && inputStack.Item != null && inputStack.Count > 0;
        bool hasOutput = HasAnyOutput();

        if (!hasInput && !hasOutput)
            return;

        if (hasCollectedAnyOutput)
        {
            CollectAllRemainingOutputs();
            ClearInputOnly();
        }
        else
        {
            if (hasInput)
            {
                AddToInventoryOrDrop(inputStack.Item, inputStack.Count);
            }

            ClearInputAndOutputs();
        }
    }

    private void CollectAllRemainingOutputs()
    {
        for (int i = outputSlots.Count - 1; i >= 0; i--)
        {
            DisassemblerOutputImageUI slot = outputSlots[i];

            if (slot == null)
                continue;

            if (slot.Item != null && slot.Count > 0)
            {
                AddToInventoryOrDrop(slot.Item, slot.Count);
            }

            Destroy(slot.gameObject);
        }

        outputSlots.Clear();

        if (outputSlotTemplate != null)
            outputSlotTemplate.Clear();
    }

    private void AddToInventoryOrDrop(Item item, int count)
    {
        if (item == null || count <= 0)
            return;

        ItemStack stack = new ItemStack(
            item,
            count,
            item.Icon,
            99
        );

        int leftover = count;

        if (inventorySystem != null)
        {
            leftover = inventorySystem.AddItemStackAndReturnLeftover(stack);
        }
        else
        {
            Debug.LogWarning("[DisassemblerSystem] InventorySystem이 없어 전부 DroppedItem으로 생성합니다.", this);
        }

        if (leftover > 0)
        {
            SpawnDroppedItems(item.Id, leftover);
        }
    }

    private void SpawnDroppedItems(string itemId, int count)
    {
        if (droppedItemPrefab == null)
        {
            Debug.LogWarning(
                "[DisassemblerSystem] droppedItemPrefab이 연결되지 않았습니다. Id: " 
                + itemId + " Count: " + count,
                this
            );
            return;
        }

        Vector3 basePosition = dropPoint != null ? dropPoint.position : transform.position;

        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.2f, 0.2f),
                0f
            );

            DroppedItem droppedItem = Instantiate(
                droppedItemPrefab,
                basePosition + randomOffset,
                Quaternion.identity
            );

            droppedItem.Init(itemId);
        }
    }

    private bool HasAnyOutput()
    {
        for (int i = 0; i < outputSlots.Count; i++)
        {
            if (outputSlots[i] != null &&
                outputSlots[i].Item != null &&
                outputSlots[i].Count > 0)
            {
                return true;
            }
        }

        return false;
    }

    private void ClearInputOnly()
    {
        inputStack = null;

        if (inputIcon != null)
        {
            inputIcon.sprite = null;
            inputIcon.enabled = false;
        }

        if (inputNameText != null)
            inputNameText.text = "";

        if (inputCountText != null)
            inputCountText.text = "";
    }

    private void ClearOutputs()
    {
        for (int i = outputSlots.Count - 1; i >= 0; i--)
        {
            if (outputSlots[i] != null)
                Destroy(outputSlots[i].gameObject);
        }

        outputSlots.Clear();

        if (outputSlotTemplate != null)
            outputSlotTemplate.Clear();
    }

    private void ClearInputAndOutputs()
    {
        ClearInputOnly();
        ClearOutputs();
        hasCollectedAnyOutput = false;
    }
}