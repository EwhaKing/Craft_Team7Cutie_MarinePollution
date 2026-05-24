using System.Collections.Generic;
using UnityEngine;

public class Turbin: Equipment
{
    private static readonly Dictionary<string, int> TurbinRecipe = new Dictionary<string, int>
    {
        { "1", 7 }  // 금속 7개
    };
    public override string Id => "115";
    public override string Name => "터빈";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => null;

    public override Dictionary<string, int> Recipe => TurbinRecipe;

}