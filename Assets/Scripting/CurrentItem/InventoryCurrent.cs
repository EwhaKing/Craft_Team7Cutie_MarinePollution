using UnityEngine;

public class InventoryCurrent : MonoBehaviour
{
    public const int OriginalSlotSize = 20;
    public const int MaxBagSize = 40;
    public const int MaxSlotSize = OriginalSlotSize + MaxBagSize;

    public ItemStack[] CurrentSlot;
    public ItemStack[] CurrentOriginalSlot;
    public ItemStack[] CurrentBag;

    private bool bagOpen = false;

    public bool IsBagOpen => bagOpen;

    public int BagSize => bagOpen ? MaxBagSize : 0;

    public int SlotSize => OriginalSlotSize + BagSize;

    private void Awake()
    {
        EnsureArraySizes();
        SyncCurrentSlot();
    }

    private void OnValidate()
    {
        EnsureArraySizes();
    }

    private void EnsureArraySizes()
    {
        if (CurrentSlot == null || CurrentSlot.Length != MaxSlotSize)
        {
            CurrentSlot = new ItemStack[MaxSlotSize];
        }

        if (CurrentOriginalSlot == null || CurrentOriginalSlot.Length != OriginalSlotSize)
        {
            CurrentOriginalSlot = new ItemStack[OriginalSlotSize];
        }

        if (CurrentBag == null || CurrentBag.Length != MaxBagSize)
        {
            CurrentBag = new ItemStack[MaxBagSize];
        }
    }

    public void OpenBag()
    {
        bagOpen = true;
        SyncCurrentSlot();

        Debug.Log("가방이 열렸습니다.", this);
    }

    public void CloseBag()
    {
        bagOpen = false;
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

        if (bagOpen)
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