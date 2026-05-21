using UnityEngine;

public static class MaterialList
{
    public static readonly Item IronPiece =
        new MaterialItem("1", "철 조각", false, null);

    public static readonly Item Rope =
        new MaterialItem("2", "밧줄", false, null);

    public static readonly Item WoodPiece =
        new MaterialItem("3", "나무 조각", false, Resources.Load<Sprite>("ItemIcon/나무 조각"));

    public static readonly Item MechanicalPiece =
        new MaterialItem("4", "기계 부품", false, null);

    public static readonly Item CopperLine =
        new MaterialItem("5", "구리선", false, null);

    public static readonly Item Cloth =
        new MaterialItem("6", "천 조각", false, null);

    public static readonly Item RubberPiece =
        new MaterialItem("7", "고무 조각", false, null);

    public static readonly Item GlassPiece =
        new MaterialItem("8", "유리 조각", false, null);

    public static readonly Item BatteryPiece =
        new MaterialItem("9", "배터리", false, null);

    public static readonly Item CoralPiece =
        new MaterialItem("10", "산호 조각", true, null);

    public static readonly Item CleanWater =
        new MaterialItem("11", "정화수", false, null);

    public static readonly Item Bubble =
        new MaterialItem("12", "공기방울", true, null);

    public static readonly Item NetPiece =
        new MaterialItem("13", "그물조각", true, null);

    private static Sprite LoadIcon(string iconName)
    {
        return Resources.Load<Sprite>("Icons/Materials/" + iconName);
    }
}