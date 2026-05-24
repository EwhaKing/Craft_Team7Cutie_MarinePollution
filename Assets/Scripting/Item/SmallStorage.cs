using System.Collections.Generic;
using UnityEngine;

public class SmallStorage : Equipment
{
    private static readonly Dictionary<string, int> SmallStorageRecipe = new Dictionary<string, int>
    {
        { "3", 3 }, // 나무 조각 3개
        { "1", 1 }  // 철 2개
    };
    public override string Id => "103";
    public override string Name => "작은저장고";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => null;

    public override Dictionary<string, int> Recipe => SmallStorageRecipe;

}