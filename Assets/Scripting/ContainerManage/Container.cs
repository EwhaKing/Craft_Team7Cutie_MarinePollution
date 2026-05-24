using UnityEngine;

public class Container : MonoBehaviour
{
    [Header("Container Info")]
    public int floor;
    public int containerIndex;

    [Header("Container Job")]
    public bool hasJob = false;

    // 이제 ContainerType enum 대신 ID만 저장
    public string containerId = "";

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

    public void SetJob(string id)
    {
        containerId = id;
        hasJob = true;

        Debug.Log($"Container 작업 부여됨: {GetContainerTypeName()} / ID: {containerId}");
    }

    public void ExecuteContainerWork()
    {
        if (!hasJob)
        {
            Debug.Log("아직 작업이 부여되지 않은 컨테이너입니다.");
            return;
        }

        switch (containerId)
        {
            case "C_1_1":
                ExecutePowerPlantWork();
                break;

            case "C_2":
                ExecuteStorageWork();
                break;

            case "C_1_2":
                ExecuteAdvancedPowerPlantWork();
                break;

            case "C_3":
                ExecuteAquariumWork();
                break;

            default:
                Debug.LogWarning($"알 수 없는 Container ID입니다: {containerId}");
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
        switch (containerId)
        {
            case "C_1_1":
                return "발전소";

            case "C_2":
                return "창고";

            case "C_1_2":
                return "상급발전소";

            case "C_3":
                return "수족관";

            default:
                return "작업 없음";
        }
    }
}