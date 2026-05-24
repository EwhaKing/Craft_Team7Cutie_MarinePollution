using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    private static readonly Dictionary<string, Item> itemsById = new Dictionary<string, Item>();

    static ItemDatabase()
    {
        Register(MaterialList.IronPiece);
        Register(MaterialList.Rope);
        Register(MaterialList.WoodPiece);
        Register(MaterialList.MechanicalPiece);
        Register(MaterialList.CopperLine);
        Register(MaterialList.Cloth);
        Register(MaterialList.RubberPiece);
        Register(MaterialList.GlassPiece);
        Register(MaterialList.BatteryPiece);
        Register(MaterialList.CoralPiece);
        Register(MaterialList.CleanWater);
        Register(MaterialList.Bubble);
        Register(MaterialList.NetPiece);

        // MaterialList.Rope와 new Rope()가 같은 아이템이면 둘 중 하나만 등록해야 합니다.
        
        Register(new Rope());

        Register(new CollectingBag());
        Register(new CoralGenerator());
        Register(new CoralModerator());

        Register(new Generator(1));
        Register(new Generator(2));

        Register(new Hook());
        Register(new InstantLight());

        Register(new OxygenBottle(1));
        Register(new OxygenBottle(2));

        Register(new SmallStorage());
        Register(new Turbin());
        Register(new OxygenGenerator());
        Register(new WaterTank());
        Register(new OxygenMerger());
    }

    private static void Register(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("[ItemDatabase] null 아이템은 등록할 수 없습니다.");
            return;
        }

        if (string.IsNullOrEmpty(item.Id))
        {
            Debug.LogWarning($"[ItemDatabase] Id가 비어 있는 아이템입니다: {item.Name}");
            return;
        }

        if (itemsById.ContainsKey(item.Id))
        {
            Debug.LogError(
                $"[ItemDatabase] 중복 Id 발견: {item.Id}\n" +
                $"기존 아이템: {itemsById[item.Id].Name}\n" +
                $"새 아이템: {item.Name}"
            );
            return;
        }

        itemsById.Add(item.Id, item);
    }

    public static Item GetItemById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogWarning("[ItemDatabase] Item Id가 비어 있습니다.");
            return null;
        }

        if (itemsById.TryGetValue(id, out Item item))
        {
            return item;
        }

        Debug.LogWarning("[ItemDatabase] 등록되지 않은 Item Id입니다: " + id);
        return null;
    }
}