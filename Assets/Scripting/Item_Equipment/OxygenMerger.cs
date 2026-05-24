using System.Collections.Generic;
using UnityEngine;

public class OxygenMerger: Equipment
{
    private static readonly Dictionary<string, int> OxygenMergerRecipe = new Dictionary<string, int>
    {
        { "1", 4 },  // 금속 조각 4개
        { "12", 2} //공기방울 2개
    };
    public override string Id => "116";
    public override string Name => "산소합성기";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => null;

    public override Dictionary<string, int> Recipe => OxygenMergerRecipe;

}