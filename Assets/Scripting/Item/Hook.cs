using System.Collections.Generic;

public class Hook : Equipment
{
    // 딕셔너리 초기화
    private static readonly Dictionary<string, int> _recipe = new()
    {
        { "1", 3 } // 철 조각 3개
        {"2",4}
    
};

    // 부모의 virtual 프로퍼티를 정상적으로 override 합니다.
    public override Dictionary<string, int> Recipe => _recipe;

    public Hook()
    {
        Id = "1";
        Name = "기본 낚시 갈고리";
        Stage = 1; // 부모의 대문자 Stage와 일치시킴
    }
}