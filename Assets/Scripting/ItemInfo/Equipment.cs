// 2. Equipment 정의 (원재료 필요)

using System.Collections.Generic;

public class Equipment : Item
{
    public string Id { get; protected set; }
    public string Name { get; protected set; }
    public int stage { get; protected set; }
    public Dictionary<string, int> Recipe { get; } // 재료 ID, 개수
}