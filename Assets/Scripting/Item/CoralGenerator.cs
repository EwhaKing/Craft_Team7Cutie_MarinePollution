using System.Collections.Generic;
using UnityEngine;

public class CoralGenerator : Equipment
{
    private static readonly Dictionary<string, int> CoralGeneratorrecipe = new Dictionary<string, int>
    {
        { "11", 3 }, // 산호 조각 10개
        { "1", 1 },  // 철 조각 10개
        {"12",5}// 정화수 5개
    };

    public override string Id => "108";
    public override string Name => "산호초 생성기";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => null;

    public override Dictionary<string, int> Recipe => CoralGeneratorrecipe;

}