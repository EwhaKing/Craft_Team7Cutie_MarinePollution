using System.Collections.Generic;

public class CollectingBag : Equipment
{
    private int AdditionalInventorySlot;
    private static readonly Dictionary<string, int> CollectingBagRecipe = new Dictionary<string, int>
    {
        { "7", 3 }, // 천 조각 5개
        { "2", 4 },  // 그물 2개
    };

    public override Dictionary<string, int> Recipe => CollectingBagRecipe;


    public CollectingBag()
    {
        Id = "106";
        Name = "비품 수집 가방";
        Rareness = false;
        Stage = 1;
    }
}