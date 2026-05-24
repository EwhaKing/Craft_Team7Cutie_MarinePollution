using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : Item
{
    public abstract string Id { get; }
    public abstract string Name { get; }
    public abstract bool Rareness { get;}
    public abstract Sprite Icon { get;}

    public abstract int Stage { get; }

    // 제작에 필요한 재료
    public abstract Dictionary<string, int> Recipe { get;  }

}