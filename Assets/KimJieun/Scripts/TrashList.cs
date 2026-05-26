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
        {
            { "1", 1 }
        }
    );

    public static readonly Item Net = new TrashItem(
        id : "t02",
        name : "폐그물",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "2", 3 }
        }
    );

    public static readonly Item Chair = new TrashItem(
        id : "t03",
        name : "버려진 의자",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "3", 1 }
        }
    );

    public static readonly Item Desk = new TrashItem(
        id : "t04",
        name : "버려진 책상",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "3", 3 }
        }
    );

    public static readonly Item Clothing = new TrashItem(
        id : "t05",
        name : "헌옷",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "6", 1 }
        }
    );

    public static readonly Item Tire = new TrashItem(
        id : "t06",
        name : "폐타이어",
        rareness : false,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "7", 3 }
        }
    );

    public static readonly Item Bottle = new TrashItem(
        id : "t07",
        name : "버려진 병",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "8", 1 }
        }
    );

    public static readonly Item Phone = new TrashItem(
        id : "t08",
        name : "버려진 핸드폰",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "9", 1 }
        }
    );

    public static readonly Item Tv = new TrashItem(
        id : "t09",
        name : "브라운관 tv",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "5", 1 },
            { "8", 2 },
            { "4", 2 }
        }
    );

    public static readonly Item Bike = new TrashItem(
        id : "t10",
        name : "자전거",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "5", 1 },
            { "1", 3 },
            { "7", 1 }
        }
    );

    public static readonly Item Fridge = new TrashItem(
        id : "t11",
        name : "냉장고",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "5", 3 },
            { "1", 1 },
            { "9", 2 }
        }
    );

    public static readonly Item Computer = new TrashItem(
        id : "t12",
        name : "노트북",
        rareness : true,
        icon : null,
        decomposeResult : new Dictionary<string, int>
        {
            { "5", 3 },
            { "1", 1 },
            { "9", 2 }
        }
    );

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
