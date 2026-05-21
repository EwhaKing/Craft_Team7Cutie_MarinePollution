using System.Collections.Generic;
using UnityEngine;

public class Garbage : Item 
{
    public string Id { get; }
    public string Name { get; }
    public int stage { get; }
    public Sprite Icon { get; private set; }
    
    // ★ 핵심 변경: 하나의 쓰레기가 가질 여러 재료와 개수 목록 ★
    public IReadOnlyDictionary<string, int> Rewards { get; }
    
    // 생성자에서 Dictionary를 통째로 넘겨받습니다.
    public Garbage(string id, string name, Dictionary<string, int> rewards, Sprite icon)
    {
        Id = id;
        Name = name;
        Rewards = rewards;
        Icon = icon;
    }
    
    // 분해 액션 함수: 이제 딕셔너리 자체를 반환합니다.
    public IReadOnlyDictionary<string, int> Deconstruct()
    {
        return Rewards;
    }
}