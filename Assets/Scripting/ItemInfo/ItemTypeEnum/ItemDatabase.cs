using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    private static readonly Dictionary<string, Item> itemsById = new Dictionary<string, Item>
    {
        { MaterialList.IronPiece.Id, MaterialList.IronPiece },
        { MaterialList.Rope.Id, MaterialList.Rope },
        { MaterialList.WoodPiece.Id, MaterialList.WoodPiece },
        { MaterialList.MechanicalPiece.Id, MaterialList.MechanicalPiece },
        { MaterialList.CopperLine.Id, MaterialList.CopperLine },
        { MaterialList.Cloth.Id, MaterialList.Cloth },
        { MaterialList.RubberPiece.Id, MaterialList.RubberPiece },
        { MaterialList.GlassPiece.Id, MaterialList.GlassPiece },
        { MaterialList.BatteryPiece.Id, MaterialList.BatteryPiece },
        { MaterialList.CoralPiece.Id, MaterialList.CoralPiece },
        { MaterialList.CleanWater.Id, MaterialList.CleanWater },
        { MaterialList.Bubble.Id, MaterialList.Bubble },
        { MaterialList.NetPiece.Id, MaterialList.NetPiece },
        
        { new Rope().Id, new Rope() },
        
    };

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