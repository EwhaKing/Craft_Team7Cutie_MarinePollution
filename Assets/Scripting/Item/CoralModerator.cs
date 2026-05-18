using System.Collections.Generic;

public class CoralModerator : Equipment
{
    private static readonly Dictionary<string, int> CoralModeratorrecipe = new Dictionary<string, int>
    {
        { "9", 3 }, // 산호 조각 20개
        { "1", 1 },  // 활성탄 10개
        {"10", 1}// 배터리 1개
    };

    public override Dictionary<string, int> Recipe => CoralModeratorrecipe;


    public CoralModerator()
    {
        Id = "110";
        Name = "산호초 중화기";
        Rareness = false;
        Stage = 1;
    }
}