using System.Collections.Generic;
using UnityEngine;

public class InstantLight : Equipment
{
    private static readonly Dictionary<string, int> InstantLightrecipe = new Dictionary<string, int>
    {
        { "9", 3 }, // 유리 조각 3개
        { "1", 1 },  // 철 3개
        {"10", 1}// 배터리 1개
    };

    public override string Id => "109";
    public override string Name => "간이 조명";

    public override bool Rareness => false;
    public override int Stage => 1;
    public override Sprite Icon => Resources.Load<Sprite>("M_Light");

    public override Dictionary<string, int> Recipe => InstantLightrecipe;


}