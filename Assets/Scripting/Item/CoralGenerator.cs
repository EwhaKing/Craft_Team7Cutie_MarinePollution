using System.Collections.Generic;

public class CoralGenerator : Equipment
{
    private static readonly Dictionary<string, int> CoralGeneratorrecipe = new Dictionary<string, int>
    {
        { "11", 3 }, // 산호 조각 10개
        { "1", 1 },  // 철 조각 10개
        {"12",5}// 정화수 5개
    };

    public override Dictionary<string, int> Recipe => CoralGeneratorrecipe;


    public CoralGenerator()
    {
        Id = "108";
        Name = "산호초 생성기";
        Rareness = false;
        Stage = 1;
    }
}