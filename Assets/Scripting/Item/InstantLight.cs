using System.Collections.Generic;

public class InstantLight : Equipment
{
    private static readonly Dictionary<string, int> InstantLightrecipe = new Dictionary<string, int>
    {
        { "9", 3 }, // 유리 조각 3개
        { "1", 1 },  // 철 3개
        {"10", 1}// 배터리 1개
    };

    public override Dictionary<string, int> Recipe => InstantLightrecipe;


    public InstantLight()
    {
        Id = "109";
        Name = "간이 조명";
        Rareness = false;
        Stage = 1;
    }
}