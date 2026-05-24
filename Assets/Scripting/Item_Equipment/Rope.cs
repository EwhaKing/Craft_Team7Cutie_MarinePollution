using System.Collections.Generic;
using UnityEngine;

public class Rope : Equipment
{
    private static readonly Dictionary<string, int> RopeRecipe = new Dictionary<string, int>
    {
        { "1", 3 }, // 그물 조각 3개
        { "2", 4 }  // 밧줄 4개
    };

    public override string Id => "102";
    public override string Name => "밧줄";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => Resources.Load<Sprite>("ItemIcon/밧줄");

    public override Dictionary<string, int> Recipe => RopeRecipe;
}