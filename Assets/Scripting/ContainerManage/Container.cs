using UnityEngine;

public enum ContainerType
{
    None,               // 아직 작업 없음
    PowerPlant,         // 발전소
    Storage,            // 창고
    AdvancedPowerPlant, // 상급발전소
    Aquarium            // 수족관
}

public class Container : MonoBehaviour
{
    [Header("Container Info")]
    public int floor;
    public int containerIndex;

    [Header("Container Job")]
    public bool hasJob = false;
    public ContainerType containerType = ContainerType.None;

    private bool canClick = false;

    public void Init(int floor, int index)
    {
        this.floor = floor;
        this.containerIndex = index;
    }

    public void GiveFirstFunction()
    {
        canClick = true;

        Debug.Log($"B{Mathf.Abs(floor)}층 Container {containerIndex} 클릭 가능");
    }

    public void RemoveFirstFunction()
    {
        canClick = false;

        Debug.Log($"B{Mathf.Abs(floor)}층 Container {containerIndex} 클릭 불가능");
    }

    private void OnMouseDown()
    {
        if (!canClick) return;

        if (ContainerManager.Instance != null)
        {
            ContainerManager.Instance.OpenContainerPanel(this);
        }
    }

    public void SetJob(ContainerType type)
    {
        containerType = type;
        hasJob = true;

        Debug.Log($"Container 작업 부여됨: {GetContainerTypeName()}");
    }

    public void ExecuteContainerWork()
    {
        if (!hasJob)
        {
            Debug.Log("아직 작업이 부여되지 않은 컨테이너입니다.");
            return;
        }

        switch (containerType)
        {
            case ContainerType.PowerPlant:
                ExecutePowerPlantWork();
                break;

            case ContainerType.Storage:
                ExecuteStorageWork();
                break;

            case ContainerType.AdvancedPowerPlant:
                ExecuteAdvancedPowerPlantWork();
                break;

            case ContainerType.Aquarium:
                ExecuteAquariumWork();
                break;
        }
    }

    void ExecutePowerPlantWork()
    {
        Debug.Log("발전소 작업 실행");
    }

    void ExecuteStorageWork()
    {
        Debug.Log("창고 작업 실행");
    }

    void ExecuteAdvancedPowerPlantWork()
    {
        Debug.Log("상급발전소 작업 실행");
    }

    void ExecuteAquariumWork()
    {
        Debug.Log("수족관 작업 실행");
    }

    public string GetContainerTypeName()
    {
        switch (containerType)
        {
            case ContainerType.PowerPlant:
                return "발전소";

            case ContainerType.Storage:
                return "창고";

            case ContainerType.AdvancedPowerPlant:
                return "상급발전소";

            case ContainerType.Aquarium:
                return "수족관";

            default:
                return "작업 없음";
        }
    }
}