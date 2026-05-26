<<<<<<< HEAD
using System.Collections.Generic;
using UnityEngine;
=======
using UnityEngine;
using System.Collections.Generic;
>>>>>>> origin/main

public class TrashItem : Item
{
    public string Id { get; }
    public string Name { get; }
    public Sprite Icon { get; }

    public bool Rareness { get; }
<<<<<<< HEAD
=======

>>>>>>> origin/main
    public Dictionary<string, int> DecomposeResult { get; }

    public TrashItem(string id, string name, bool rareness, Sprite icon, Dictionary<string, int> decomposeResult)
    {
        Id = id;
        Name = name;
        Rareness = rareness;
        Icon = icon;
        DecomposeResult = decomposeResult;
    }
<<<<<<< HEAD
}
=======
    
}
>>>>>>> origin/main
