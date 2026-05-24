using System.Collections.Generic;
using UnityEngine;

public class Hook : Equipment
{
    private static readonly Dictionary<string, int> Hookrecipe = new Dictionary<string, int>
    {
        { "1", 3 }, // 철 조각 3개
        { "2", 1 }  // 밧줄 1개
    };
    public override string Id => "101";
    public override string Name => "갈고리";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => null;
    public override Dictionary<string, int> Recipe => Hookrecipe;

}
