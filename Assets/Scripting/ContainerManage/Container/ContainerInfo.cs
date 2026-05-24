using System.Collections.Generic;
using UnityEngine;

public class ContainerInfo
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public Dictionary<string, int> Recipe { get; private set; }

    public ContainerInfo(string id, string name, Sprite icon, Dictionary<string, int> recipe)
    {
        Id = id;
        Name = name;
        Icon = icon;
        Recipe = recipe;
    }
}

public static class ContainerInfoData
{
    private static readonly Dictionary<string, int> PowerPlantRecipe = new Dictionary<string, int>
    {
        { "104_1", 4 },
        
    };

    private static readonly Dictionary<string, int> StorageRecipe = new Dictionary<string, int>
    {
        { "1", 5 },
        { "3", 2 }
    };

    private static readonly Dictionary<string, int> AdvancedPowerPlantRecipe = new Dictionary<string, int>
    {
        { "1", 8 },
        { "2", 3 },
        { "4", 1 }
    };

    private static readonly Dictionary<string, int> AquariumRecipe = new Dictionary<string, int>
    {
        { "3", 4 },
        { "5", 2 }
    };

    public static readonly Dictionary<string, ContainerInfo> ContainerInfos =
        new Dictionary<string, ContainerInfo>
        {
            {
                "C_1_1",
                new ContainerInfo(
                    "C_1_1",
                    "발전소",
                    null,
                    PowerPlantRecipe
                )
            },
            {
                "C_2",
                new ContainerInfo(
                    "C_2",
                    "창고",
                    null,
                    StorageRecipe
                )
            },
            {
                "C_1_2",
                new ContainerInfo(
                    "C_1_2",
                    "상급발전소",
                    null,
                    AdvancedPowerPlantRecipe
                )
            },
            {
                "C_3",
                new ContainerInfo(
                    "C_3",
                    "수족관",
                    null,
                    AquariumRecipe
                )
            }
        };

    public static ContainerInfo GetInfo(string id)
    {
        if (ContainerInfos.ContainsKey(id))
        {
            return ContainerInfos[id];
        }

        return null;
    }
}