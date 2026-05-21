using UnityEngine;

public class InventoryCurrent : MonoBehaviour
{
    public const int OriginalSlotSize = 20;
    public const int MaxBagSize = 40;
    public const int MaxSlotSize = OriginalSlotSize + MaxBagSize;

    public ItemStack[] CurrentSlot = new ItemStack[MaxSlotSize];
    public ItemStack[] CurrentOriginalSlot = new ItemStack[OriginalSlotSize];
    public ItemStack[] CurrentBag = new ItemStack[MaxBagSize];

    private bool bagOpen = false;

    public bool IsBagOpen => bagOpen;

    public int BagSize => bagOpen ? MaxBagSize : 0;

    public int SlotSize => OriginalSlotSize + BagSize;

    public void OpenBag()
    {
        bagOpen = true;
        SyncCurrentSlot();
    }

    public void CloseBag()
    {
        bagOpen = false;
        SyncCurrentSlot();

        Debug.Log("가방이 닫혔습니다.");
    }

    public void SyncCurrentSlot()
    {
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
}