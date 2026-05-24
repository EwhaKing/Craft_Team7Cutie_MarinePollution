using System.Collections.Generic;
using UnityEngine;

public class OxygenGenerator: Equipment
{
    private static readonly Dictionary<string, int> OxygenGeneratorRecipe = new Dictionary<string, int>
    {
        { "115", 3 }, // 터빈 1개
        { "116", 1 }  // 산소 합성기 1개
    };
    public override string Id => "113";
    public override string Name => "산소 발생기";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => null;

    public override Dictionary<string, int> Recipe => OxygenGeneratorRecipe;

}