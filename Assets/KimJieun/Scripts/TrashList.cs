<<<<<<< HEAD
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
=======
using UnityEngine;
using System.Collections.Generic;

public static class TrashList
{
    public static readonly Item Can = new TrashItem(
        id : "t01",
        name : "찌그러진 캔",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "1", 1 }
        }
    );

    public static readonly Item Net = new TrashItem(
<<<<<<< HEAD
        id: "T002",
        name: "폐그물",
        rareness: false,
        icon: GetIcon("T002"),
        decomposeResult: new Dictionary<string, int>
=======
        id : "t02",
        name : "폐그물",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "2", 3 }
        }
    );

    public static readonly Item Chair = new TrashItem(
<<<<<<< HEAD
        id: "T003",
        name: "버려진 의자",
        rareness : false,
        icon: GetIcon("T003"),
        decomposeResult: new Dictionary<string, int>
=======
        id : "t03",
        name : "버려진 의자",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "3", 1 }
        }
    );

    public static readonly Item Desk = new TrashItem(
<<<<<<< HEAD
        id: "T004",
        name: "버려진 책상",
        rareness : false,
        icon: GetIcon("T004"),
        decomposeResult: new Dictionary<string, int>
=======
        id : "t04",
        name : "버려진 책상",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "3", 3 }
        }
    );

    public static readonly Item Clothing = new TrashItem(
<<<<<<< HEAD
        id: "T005",
        name: "헌옷",
        rareness : false,
        icon: GetIcon("T005"),
        decomposeResult: new Dictionary<string, int>
=======
        id : "t05",
        name : "헌옷",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "6", 1 }
        }
    );

    public static readonly Item Tire = new TrashItem(
<<<<<<< HEAD
        id: "T006",
        name: "폐타이어",
        rareness : false,
        icon: GetIcon("T006"),
        decomposeResult: new Dictionary<string, int>
=======
        id : "t06",
        name : "폐타이어",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "7", 3 }
        }
    );

    public static readonly Item Bottle = new TrashItem(
<<<<<<< HEAD
        id: "T007",
        name: "버려진 병",
        rareness : false,
        icon: GetIcon("T007"),
        decomposeResult: new Dictionary<string, int>
=======
        id : "t07",
        name : "버려진 병",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "8", 1 }
        }
    );

    public static readonly Item Phone = new TrashItem(
<<<<<<< HEAD
        id: "T008",
        name: "버려진 핸드폰",
        rareness : false,
        icon: GetIcon("T008"),
        decomposeResult: new Dictionary<string, int>
=======
        id : "t08",
        name : "버려진 핸드폰",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "9", 1 }
        }
    );

<<<<<<< HEAD
    public static readonly Item Television = new TrashItem(
        id: "T009",
        name: "브라운관 tv",
        rareness : false,
        icon: GetIcon("T009"),
        decomposeResult: new Dictionary<string, int>
=======
    public static readonly Item Tv = new TrashItem(
        id : "t09",
        name : "브라운관 tv",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "5", 1 },
            { "8", 2 },
            { "4", 2 }
        }
    );
<<<<<<< HEAD
    
    public static readonly Item Bike = new TrashItem(
        id: "T010",
        name: "자전거",
        rareness : false,
        icon: GetIcon("T010"),
        decomposeResult: new Dictionary<string, int>
=======

    public static readonly Item Bike = new TrashItem(
        id : "t10",
        name : "자전거",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "5", 1 },
            { "1", 3 },
            { "7", 1 }
        }
    );

    public static readonly Item Fridge = new TrashItem(
<<<<<<< HEAD
        id: "T011",
        name: "냉장고",
        rareness : false,
        icon: GetIcon("T011"),
        decomposeResult: new Dictionary<string, int>
=======
        id : "t11",
        name : "냉장고",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "5", 3 },
            { "1", 1 },
            { "9", 2 }
        }
    );

<<<<<<< HEAD
    public static readonly Item Laptop = new TrashItem(
        id: "T012",
        name: "노트북",
        rareness : false,
        icon: GetIcon("T012"),
        decomposeResult: new Dictionary<string, int>
=======
    public static readonly Item Computer = new TrashItem(
        id : "t12",
        name : "노트북",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
>>>>>>> origin/main
        {
            { "5", 3 },
            { "1", 1 },
            { "9", 2 }
        }
    );
<<<<<<< HEAD
}
=======

    public static readonly Item Coral = new TrashItem(
        id : "t13",
        name : "죽은 산호 조각",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "10", 1 }
        }
    );

    public static readonly Item Water = new TrashItem(
        id : "t14",
        name : "정화수",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "11", 1 }
        }
    );

    public static readonly Item Bubble = new TrashItem(
        id : "t15",
        name : "공기방울",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "12", 1 }
        }
    );
}
>>>>>>> origin/main
