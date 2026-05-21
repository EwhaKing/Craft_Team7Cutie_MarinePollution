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

    private void Awake()
    {
        SyncCurrentSlot();
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

        string itemText = GetItemDisplayName(stack.Item);

        Debug.Log($"{slotName} {index}: {itemText} x{stack.Count}", this);
    }

    private string GetItemDisplayName(Item item)
    {
        // Item에 Name이 있다면 이 줄을 사용하세요.
        return item.Name;

        // 만약 item.Name에서 에러가 나면 위 줄을 지우고 아래 중 하나로 바꾸세요.
        // return item.Id.ToString();
        // return item.name;
        // return "아이템";
    }
}