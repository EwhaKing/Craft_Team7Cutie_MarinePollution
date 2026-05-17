using System.Collections.Generic;

public class Rope : Equipment
{
    private static readonly Dictionary<string, int> RopeRecipe = new Dictionary<string, int>
    {
        { "1", 3 }, // 그물 조각 3개
        { "2", 4 }  // 밧줄 4개
    };

    public override Dictionary<string, int> Recipe => RopeRecipe;


    public Rope()
    {
        Id = "102";
        Name = "밧줄";
        Rareness = false;
        Stage = 1;
    }
}