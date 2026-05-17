using System.Collections.Generic;

public class SmallStorage : Equipment
{
    private static readonly Dictionary<string, int> SmallStorageRecipe = new Dictionary<string, int>
    {
        { "3", 3 }, // 나무 조각 3개
        { "1", 1 }  // 철 2개
    };

    public override Dictionary<string, int> Recipe => SmallStorageRecipe;


    public SmallStorage()
    {
        Id = "103";
        Name = "밧줄";
        Rareness = false;
        Stage = 1;
    }
}