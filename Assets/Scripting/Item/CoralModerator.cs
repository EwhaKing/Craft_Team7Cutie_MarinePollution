using System.Collections.Generic;
using UnityEngine;

public class CoralModerator : Equipment
{
    private static readonly Dictionary<string, int> CoralModeratorrecipe = new Dictionary<string, int>
    {
        { "9", 3 }, // 산호 조각 20개
        { "1", 1 },  // 활성탄 10개
        {"10", 1}// 배터리 1개
    };
    public override string Id => "110";
    public override string Name => "산호초 중화기";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => null;
    public override Dictionary<string, int> Recipe => CoralModeratorrecipe;


}