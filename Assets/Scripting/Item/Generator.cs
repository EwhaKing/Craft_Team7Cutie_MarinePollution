using System.Collections.Generic;

public class Generator : Equipment
{
    private static readonly Dictionary<string, int> GeneratorStage1Recipe = new Dictionary<string, int>
    {
        { "1", 5 },   // 철 조각 ×5
        { "3", 10 },  // 나무 조각 ×10
        { "4", 2 }    // 기계 부품 ×2
    };

    private static readonly Dictionary<string, int> GeneratorStage2Recipe = new Dictionary<string, int>
    {
        { "1", 10 },  // 철 조각 ×10
        { "5", 5 },   // 구리선 ×5
        { "4", 5 }    // 기계 부품 ×5
    };

    public override Dictionary<string, int> Recipe { get; protected set; }

    public string Description { get; private set; }
    public int MaxWorkingDeviceCount { get; private set; }
    public bool CanUseDisassembler { get; private set; }
    public bool CanUseLight { get; private set; }
    public string FuelType { get; private set; }
    public int MaxFuelAmount { get; private set; }

    public Generator(int stage)
    {
        Rareness = false;
        Stage = stage;

        if (Stage == 1)
        {
            Id = "104_1";
            Name = "초기 발전기";
            Description = "지상 시설에 전기를 공급하는 초반용 발전기.";

            Recipe = GeneratorStage1Recipe;

            CanUseDisassembler = true;
            CanUseLight = true;
            MaxWorkingDeviceCount = 1;
            FuelType = "나무";
            MaxFuelAmount = 10;
        }
        else if (Stage == 2)
        {
            Id = "104_2";
            Name = "발전기2";
            Description = "출력이 향상된 중형 발전기.";

            Recipe = GeneratorStage2Recipe;

            CanUseDisassembler = true;
            CanUseLight = true;
            MaxWorkingDeviceCount = 2;
            FuelType = "나무";
            MaxFuelAmount = 20;
        }
        else
        {
            Id = "104_1";
            Name = "초기 발전기";
            Description = "지상 시설에 전기를 공급하는 초반용 발전기.";

            Stage = 1;
            Recipe = GeneratorStage1Recipe;

            CanUseDisassembler = true;
            CanUseLight = true;
            MaxWorkingDeviceCount = 1;
            FuelType = "나무";
            MaxFuelAmount = 10;
        }
    }
}