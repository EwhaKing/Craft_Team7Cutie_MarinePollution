using System.Collections.Generic;
using UnityEngine;

public class CollectingBag : Equipment
{
    private int AdditionalInventorySlot;
    private static readonly Dictionary<string, int> CollectingBagRecipe = new Dictionary<string, int>
    {
        { "7", 3 }, // 천 조각 5개
        { "2", 4 },  // 그물 2개
    };
    public override string Id => "106";
    public override string Name => "비품 수집 가방";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => null;
    public override Dictionary<string, int> Recipe => CollectingBagRecipe;

}