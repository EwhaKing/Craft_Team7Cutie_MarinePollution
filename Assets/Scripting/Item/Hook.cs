using System.Collections.Generic;

public class Hook : Equipment
{
    private static readonly Dictionary<string, int> Hookrecipe = new Dictionary<string, int>
    {
        { "1", 3 }, // 철 조각 3개
        { "2", 1 }  // 밧줄 1개
    };

    public override Dictionary<string, int> Recipe => Hookrecipe;


    public Hook()
    {
        Id = "101";
        Name = "밧줄";
        Rareness = false;
        Stage = 1;
    }
}