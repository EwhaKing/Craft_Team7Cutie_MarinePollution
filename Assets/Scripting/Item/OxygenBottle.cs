using System.Collections.Generic;
using UnityEngine;

public class OxygenBottle : Equipment
{
    private static readonly Dictionary<string, int> OxygenBottleStage1Recipe = new Dictionary<string, int>
    {
        { "1", 3 },   // 철 조각 ×3
        { "13", 1 },  // 공기방울 ×1
        { "8", 2 }    // 고무조각 ×2
    };

    private static readonly Dictionary<string, int> OxygenBottleStage2Recipe = new Dictionary<string, int>
    {
        { "1", 8 },   // 철 조각 ×8
        { "13", 5 },  // 공기방울 ×5
        { "8", 5 }    // 고무조각 ×5
    };

    private string id;
    private string itemName;
    private int stage;
    private int breatheTime;
    private Dictionary<string, int> recipe;

    public override string Id => id;
    public override string Name => itemName;
    public override Sprite Icon => null;

    public override bool Rareness => false;
    public override int Stage => stage;
    public override Dictionary<string, int> Recipe => recipe;

    public string Description { get; private set; }
    public int BreatheTime => breatheTime;

    public OxygenBottle(int stage)
    {
        if (stage == 1)
        {
            this.stage = 1;

            id = "105_1";
            itemName = "산소통Lv1";
            Description = "잠수 시간을 조금 늘려주는 기본 산소통.";

            recipe = OxygenBottleStage1Recipe;

            breatheTime = 30;
        }
        else if (stage == 2)
        {
            this.stage = 2;

            id = "105_2";
            itemName = "산소통Lv2";
            Description = "잠수 시간을 크게 늘려주는 향상된 산소통.";

            recipe = OxygenBottleStage2Recipe;

            breatheTime = 90;
        }
        else
        {
            this.stage = 1;

            id = "105_1";
            itemName = "산소통Lv1";
            Description = "잠수 시간을 조금 늘려주는 기본 산소통.";

            recipe = OxygenBottleStage1Recipe;

            breatheTime = 30;
        }
    }
}