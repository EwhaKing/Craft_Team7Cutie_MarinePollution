using System;

// 1. 일반 클래스로 선언 (abstract를 제거하여 new 생성 가능하게 변경)
public class MaterialItem : Item
{
    // 인터페이스 구현
    public string Id { get; }
    public string Name { get; }
    public bool Rareness { get; }


    // 2. 매개변수 3개를 정상적으로 받는 생성자 정의
    public MaterialItem(string id, string name, bool rareness)
    {
        Id = id;
        Name = name;
        Rareness = rareness;
    }
    
    Item IronPiece = new MaterialItem("1", "철 조각", false);
    Item Rope = new MaterialItem("2", "밧줄", false);
    Item WoodPiece = new MaterialItem("3","나무조각", false);
    Item MechanicalPiece = new MaterialItem("4", "기계 부품", false);
    Item CopperLine = new MaterialItem("5", "구리선", false);
    Item Cloth = new MaterialItem("6", "천 조각", false);
    Item RubberPiece = new MaterialItem("7", "고무 조각", false);
    Item GlassPiece = new MaterialItem("8", "유리 조각", false);
    Item BatteryPiece = new MaterialItem("9", "배터리", false);
    Item CoralPiece = new MaterialItem("10", "산호 조각", true);
    Item CleanWater = new MaterialItem("11","정화수", false);
    Item Bubble = new MaterialItem("12", "공기방울", true);
}

