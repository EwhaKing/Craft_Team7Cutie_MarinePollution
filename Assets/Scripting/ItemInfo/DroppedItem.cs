using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [Header("Item Type")]
    [SerializeField] private ItemCategory itemCategory;

    [Header("Material")]
    [SerializeField] private MaterialType materialType;

    [Header("Equipment")]
    [SerializeField] private EquipmentType equipmentType;

    [Header("Garbage")]
    [SerializeField] private GarbageType garbageType;

    [Header("Stack")]
    [SerializeField] private int count = 1;
    [SerializeField] private int maxStack = 99;

    private ItemStack itemStack;

    public ItemStack ItemStack => itemStack;

    private void Awake()
    {
        Item item = GetItem();

        if (item == null)
        {
            Debug.LogError(
                "[DroppedItem] GetItem() 결과가 null입니다.\n" +
                "ItemCategory: " + itemCategory + "\n" +
                "MaterialType: " + materialType + "\n" +
                "EquipmentType: " + equipmentType + "\n" +
                "GarbageType: " + garbageType,
                this
            );
            return;
        }

        itemStack = new ItemStack(
            item,
            count,
            item.Icon,
            maxStack
        );

        Debug.Log(
            "[DroppedItem] Awake에서 ItemStack 생성 완료\n" +
            "Id: " + item.Id + "\n" +
            "Name: " + item.Name + "\n" +
            "Count: " + count + "\n" +
            "Icon null?: " + (item.Icon == null) + "\n" +
            "Icon name: " + (item.Icon != null ? item.Icon.name : "NULL"),
            this
        );
    }

    public void Init(Item item, int count, Sprite icon = null, int maxStack = 99)
    {
        if (item == null)
        {
            Debug.LogError("[DroppedItem] Init 실패: item이 null입니다.", this);
            itemStack = null;
            return;
        }

        Sprite finalIcon = icon != null ? icon : item.Icon;

        itemStack = new ItemStack(
            item,
            count,
            finalIcon,
            maxStack
        );

        Debug.Log(
            "[DroppedItem] Init 완료\n" +
            "Id: " + item.Id + "\n" +
            "Name: " + item.Name + "\n" +
            "Count: " + count + "\n" +
            "Icon null?: " + (finalIcon == null) + "\n" +
            "Icon name: " + (finalIcon != null ? finalIcon.name : "NULL"),
            this
        );
    }

    public void Init(ItemStack stack)
    {
        itemStack = stack;
    }

    public void Pickup()
    {
        Debug.Log("6. Pickup 실행됨. 삭제합니다.", this);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("1. Player 감지됨", this);

        if (itemStack == null)
        {
            Debug.LogWarning("2. 실패: itemStack이 null입니다.", this);
            return;
        }

        Debug.Log("2. itemStack 있음", this);

        PlayerItemPicker picker = other.GetComponent<PlayerItemPicker>();

        if (picker == null)
        {
            Debug.LogWarning("3. 실패: PlayerItemPicker가 없습니다.", other);
            return;
        }

        Debug.Log("3. PlayerItemPicker 찾음", this);

        bool picked = picker.TryPickup(itemStack);

        Debug.Log("4. TryPickup 결과: " + picked, this);

        if (picked)
        {
            Debug.Log("5. Pickup 실행 직전", this);
            Pickup();
        }
    }

    private Item GetItem()
    {
        switch (itemCategory)
        {
            case ItemCategory.Material:
                return GetMaterial(materialType);

            case ItemCategory.Equipment:
                return GetEquipment(equipmentType);

            case ItemCategory.Garbage:
                return GetGarbage(garbageType);

            default:
                return null;
        }
    }

    private Item GetMaterial(MaterialType type)
    {
        switch (type)
        {
            case MaterialType.IronPiece:
                return MaterialList.IronPiece;
            case MaterialType.Rope:
                return MaterialList.Rope;
            case MaterialType.WoodPiece:
                return MaterialList.WoodPiece;
            case MaterialType.MechanicalPiece:
                return MaterialList.MechanicalPiece;
            case MaterialType.CopperLine:
                return MaterialList.CopperLine;
            case MaterialType.Cloth:
                return MaterialList.Cloth;
            case MaterialType.RubberPiece:
                return MaterialList.RubberPiece;
            case MaterialType.GlassPiece:
                return MaterialList.GlassPiece;
            case MaterialType.BatteryPiece:
                return MaterialList.BatteryPiece;
            case MaterialType.CoralPiece:
                return MaterialList.CoralPiece;
            case MaterialType.CleanWater:
                return MaterialList.CleanWater;
            case MaterialType.Bubble:
                return MaterialList.Bubble;
            case MaterialType.NetPiece:
                return MaterialList.NetPiece;
            default:
                return null;
        }
    }

    private Item GetEquipment(EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.None:
                return null;

            default:
                return null;
        }
    }

    private Item GetGarbage(GarbageType type)
    {
        switch (type)
        {
            case GarbageType.PlasticBottle:
                return null;
            case GarbageType.RustyCan:                
                return null;                
            case GarbageType.BrokenGlass:   
                return null;               
            default:
                return null;
        }
    }
}