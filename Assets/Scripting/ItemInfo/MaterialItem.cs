using UnityEngine;

// 1. 일반 클래스로 선언 (abstract를 제거하여 new 생성 가능하게 변경)
public class MaterialItem : Item
{
    // 인터페이스 구현
    public string Id { get; }
    public string Name { get; }
    public bool Rareness { get; }
    public Sprite Icon { get; }

    // 2. 매개변수 3개를 정상적으로 받는 생성자 정의
    public MaterialItem(string id, string name, bool rareness, Sprite icon)
    {
        Id = id;
        Name = name;
        Rareness = rareness;
        Icon = icon;

    }
    
    
}

