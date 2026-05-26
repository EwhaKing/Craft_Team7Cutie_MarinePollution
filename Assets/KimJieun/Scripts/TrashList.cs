using System.Collections.Generic;
using UnityEngine;

public static class TrashList
{
    private static Sprite GetIcon(string id)
    {
        return Resources.Load<Sprite>($"Trash/{id}");
    }

    public static readonly Item Can = new TrashItem(
        id: "T001",
        name: "찌그러진 캔",
        rareness : false,
        icon: GetIcon("T001"),
        decomposeResult: new Dictionary<string, int>
        {
            { "1", 1 }
        }
    );

    public static readonly Item Net = new TrashItem(
        id: "T002",
        name: "폐그물",
        rareness: false,
        icon: GetIcon("T002"),
        decomposeResult: new Dictionary<string, int>
        {
            { "2", 3 }
        }
    );

    public static readonly Item Chair = new TrashItem(
        id: "T003",
        name: "버려진 의자",
        rareness : false,
        icon: GetIcon("T003"),
        decomposeResult: new Dictionary<string, int>
        {
            { "3", 1 }
        }
    );

    public static readonly Item Desk = new TrashItem(
        id: "T004",
        name: "버려진 책상",
        rareness : false,
        icon: GetIcon("T004"),
        decomposeResult: new Dictionary<string, int>
        {
            { "3", 3 }
        }
    );

    public static readonly Item Clothing = new TrashItem(
        id: "T005",
        name: "헌옷",
        rareness : false,
        icon: GetIcon("T005"),
        decomposeResult: new Dictionary<string, int>
        {
            { "6", 1 }
        }
    );

    public static readonly Item Tire = new TrashItem(
        id: "T006",
        name: "폐타이어",
        rareness : false,
        icon: GetIcon("T006"),
        decomposeResult: new Dictionary<string, int>
        {
            { "7", 3 }
        }
    );

    public static readonly Item Bottle = new TrashItem(
        id: "T007",
        name: "버려진 병",
        rareness : false,
        icon: GetIcon("T007"),
        decomposeResult: new Dictionary<string, int>
        {
            { "8", 1 }
        }
    );

    public static readonly Item Phone = new TrashItem(
        id: "T008",
        name: "버려진 핸드폰",
        rareness : false,
        icon: GetIcon("T008"),
        decomposeResult: new Dictionary<string, int>
        {
            { "9", 1 }
        }
    );

    public static readonly Item Television = new TrashItem(
        id: "T009",
        name: "브라운관 tv",
        rareness : false,
        icon: GetIcon("T009"),
        decomposeResult: new Dictionary<string, int>
        {
            { "5", 1 },
            { "8", 2 },
            { "4", 2 }
        }
    );

    public static readonly Item Bike = new TrashItem(
        id: "T010",
        name: "자전거",
        rareness : false,
        icon: GetIcon("T010"),
        decomposeResult: new Dictionary<string, int>
        {
            { "5", 1 },
            { "1", 3 },
            { "7", 1 }
        }
    );

    public static readonly Item Fridge = new TrashItem(
        id: "T011",
        name: "냉장고",
        rareness : false,
        icon: GetIcon("T011"),
        decomposeResult: new Dictionary<string, int>
        {
            { "5", 3 },
            { "1", 1 },
            { "9", 2 }
        }
    );

    public static readonly Item Laptop = new TrashItem(
        id: "T012",
        name: "노트북",
        rareness : false,
        icon: GetIcon("T012"),
        decomposeResult: new Dictionary<string, int>
        {
            { "5", 3 },
            { "1", 1 },
            { "9", 2 }
        }
    );
}
