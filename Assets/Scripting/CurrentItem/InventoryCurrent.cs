using UnityEngine;

public class InventoryCurrent : MonoBehaviour
{
    public const int OriginalSlotSize = 20;
    public const int MaxBagSize = 40;
    public const int MaxSlotSize = OriginalSlotSize + MaxBagSize;

    private static ItemStack[] sharedCurrentSlot;
    private static ItemStack[] sharedOriginalSlot;
    private static ItemStack[] sharedBag;

    private static bool sharedBagOpen = false;

    public ItemStack[] CurrentSlot;
    public ItemStack[] CurrentOriginalSlot;
    public ItemStack[] CurrentBag;

    public bool IsBagOpen => sharedBagOpen;

    public int BagSize => sharedBagOpen ? MaxBagSize : 0;

    public int SlotSize => OriginalSlotSize + BagSize;

    private void Awake()
    {
        EnsureArraySizes();
        SyncCurrentSlot();

        Debug.Log("[InventoryCurrent] static 인벤토리 연결 완료: " + gameObject.name, this);
    }

    private void OnValidate()
    {
        EnsureArraySizes();
    }

    private void EnsureArraySizes()
    {
        if (sharedCurrentSlot == null || sharedCurrentSlot.Length != MaxSlotSize)
        {
            sharedCurrentSlot = new ItemStack[MaxSlotSize];
        }

        if (sharedOriginalSlot == null || sharedOriginalSlot.Length != OriginalSlotSize)
        {
            sharedOriginalSlot = new ItemStack[OriginalSlotSize];
        }

        if (sharedBag == null || sharedBag.Length != MaxBagSize)
        {
            sharedBag = new ItemStack[MaxBagSize];
        }

        CurrentSlot = sharedCurrentSlot;
        CurrentOriginalSlot = sharedOriginalSlot;
        CurrentBag = sharedBag;
    }

    public void OpenBag()
    {
        sharedBagOpen = true;
        SyncCurrentSlot();

        Debug.Log("가방이 열렸습니다.", this);
    }

    public void CloseBag()
    {
        sharedBagOpen = false;
        SyncCurrentSlot();

        Debug.Log("가방이 닫혔습니다.", this);
    }

    public void SyncCurrentSlot()
    {
        EnsureArraySizes();

        for (int i = 0; i < CurrentSlot.Length; i++)
        {
            CurrentSlot[i] = null;
        }

        for (int i = 0; i < OriginalSlotSize; i++)
        {
            CurrentSlot[i] = CurrentOriginalSlot[i];
        }

        if (sharedBagOpen)
        {
            for (int i = 0; i < MaxBagSize; i++)
            {
                CurrentSlot[OriginalSlotSize + i] = CurrentBag[i];
            }
        }
    }

    public void PrintInventory()
    {
        EnsureArraySizes();

        Debug.Log("===== 현재 인벤토리 =====", this);

        Debug.Log("----- 기본 슬롯 -----", this);

        for (int i = 0; i < CurrentOriginalSlot.Length; i++)
        {
            PrintSlot("기본 슬롯", i, CurrentOriginalSlot[i]);
        }

        Debug.Log("----- 가방 슬롯 -----", this);

        for (int i = 0; i < CurrentBag.Length; i++)
        {
            PrintSlot("가방 슬롯", i, CurrentBag[i]);
        }

        Debug.Log("====================", this);
    }

    private void PrintSlot(string slotName, int index, ItemStack stack)
    {
        if (stack == null)
        {
            Debug.Log($"{slotName} {index}: 비어 있음", this);
            return;
        }

        if (stack.Item == null)
        {
            Debug.LogWarning($"{slotName} {index}: ItemStack은 있지만 Item이 null입니다. Count: {stack.Count}", this);
            return;
        }

        Debug.Log($"{slotName} {index}: {stack.Item.Name} x{stack.Count}", this);
    }
}