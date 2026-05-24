using System.Collections.Generic;
using UnityEngine;

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

    private string id;
    private string itemName;
    private int stage;
    private Dictionary<string, int> recipe;

    public override string Id => id;
    public override string Name => itemName;
    public override Sprite Icon => null;

    public override bool Rareness => false;
    public override int Stage => stage;
    public override Dictionary<string, int> Recipe => recipe;

    public string Description { get; private set; }
    public int MaxWorkingDeviceCount { get; private set; }
    public bool CanUseDisassembler { get; private set; }
    public bool CanUseLight { get; private set; }
    public string FuelType { get; private set; }
    public int MaxFuelAmount { get; private set; }

    public Generator(int stage)
    {
        if (stage == 1)
        {
            this.stage = 1;

            id = "104_1";
            itemName = "초기 발전기";
            Description = "지상 시설에 전기를 공급하는 초반용 발전기.";

            recipe = GeneratorStage1Recipe;

            CanUseDisassembler = true;
            CanUseLight = true;
            MaxWorkingDeviceCount = 1;
            FuelType = "나무";
            MaxFuelAmount = 10;
        }
        else if (stage == 2)
        {
            this.stage = 2;

            id = "104_2";
            itemName = "발전기2";
            Description = "출력이 향상된 중형 발전기.";

            recipe = GeneratorStage2Recipe;

            CanUseDisassembler = true;
            CanUseLight = true;
            MaxWorkingDeviceCount = 2;
            FuelType = "나무";
            MaxFuelAmount = 20;
        }
        else
        {
            this.stage = 1;

            id = "104_1";
            itemName = "초기 발전기";
            Description = "지상 시설에 전기를 공급하는 초반용 발전기.";

            recipe = GeneratorStage1Recipe;

            CanUseDisassembler = true;
            CanUseLight = true;
            MaxWorkingDeviceCount = 1;
            FuelType = "나무";
            MaxFuelAmount = 10;
        }
    }
}