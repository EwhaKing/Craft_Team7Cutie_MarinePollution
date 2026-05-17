using System.Collections.Generic;

public class OxygenBottle : Equipment
{
    private int BreatheTime;
    private static readonly Dictionary<string, int> OxygenBottleStage1Recipe = new Dictionary<string, int>
    {
        { "1", 3 },   // 철 조각 ×3
        { "13", 1 },  // 공기방울 ×1
        { "8", 2 }    // 고무조각 ×2
    };

    private static readonly Dictionary<string, int> OxygenBottleStage2Recipe = new Dictionary<string, int>
    {
        { "1", 8 },  // 철 조각 ×8
        { "13", 5 },   // 공기방울 ×5
        { "8", 5 }    // 고무조각 ×5
    };

    public override Dictionary<string, int> Recipe { get; protected set; }

    public string Description { get; private set; }
    public int MaxWorkingDeviceCount { get; private set; }
    public bool CanUseDisassembler { get; private set; }
    public bool CanUseLight { get; private set; }
    public string FuelType { get; private set; }
    public int MaxFuelAmount { get; private set; }

    public OxygenBottle(int stage)
    {
        Rareness = false;
        Stage = stage;

        if (Stage == 1)
        {
            Id = "105_1";
            Name = "산소통Lv1";
            Description = "지상 시설에 전기를 공급하는 초반용 발전기.";

            Recipe = OxygenBottleStage1Recipe;

            CanUseDisassembler = true;
            CanUseLight = true;
            MaxWorkingDeviceCount = 1;
            FuelType = "나무";
            MaxFuelAmount = 10;
            BreatheTime = 30;
        }
        else if (Stage == 2)
        {
            Id = "105_2";
            Name = "산소통Lv2";
            Description = "출력이 향상된 중형 발전기.";

            Recipe = OxygenBottleStage2Recipe;

            CanUseDisassembler = true;
            CanUseLight = true;
            MaxWorkingDeviceCount = 2;
            FuelType = "나무";
            MaxFuelAmount = 20;
            BreatheTime = 90;
        }
        else
        {
            Id = "104_1";
            Name = "초기 발전기";
            Description = "지상 시설에 전기를 공급하는 초반용 발전기.";

            Stage = 1;
            Recipe = OxygenBottleStage1Recipe;

            CanUseDisassembler = true;
            CanUseLight = true;
            MaxWorkingDeviceCount = 1;
            FuelType = "나무";
            MaxFuelAmount = 10;
        }
    }
}