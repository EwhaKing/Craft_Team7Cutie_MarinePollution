using UnityEngine;
public interface Item
{
    string Id { get; }//고유 식별자(저장에 사용)
    string Name { get; }
    Sprite Icon { get; }
}
