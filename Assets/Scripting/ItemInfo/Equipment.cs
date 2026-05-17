using System.Collections.Generic;

public class Equipment : Item
{
    public string Id { get; protected set; }
    public string Name { get; protected set; }
    public bool Rareness { get; protected set; }

    public int Stage { get; protected set; }

    // 제작에 필요한 재료
    public virtual Dictionary<string, int> Recipe { get; protected set; }
        = new Dictionary<string, int>();


}