using System.Collections.Generic;
using UnityEngine;

public class WaterTank : Equipment
{
    private static readonly Dictionary<string, int> WaterTankRecipe = new Dictionary<string, int>
    {
        { "8", 3 }, // 유리 조각 6개
        { "1", 1 }  // 금속 2개
    };
    public override string Id => "114";
    public override string Name => "수조";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => null;

    public override Dictionary<string, int> Recipe => WaterTankRecipe;

}