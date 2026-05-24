using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public static ContainerManager Instance;

    [Header("Containers By Floor")]
    public List<Container> floorB1Containers = new List<Container>();
    public List<Container> floorB2Containers = new List<Container>();
    public List<Container> floorB3Containers = new List<Container>();
    public List<Container> floorB4Containers = new List<Container>();

    [Header("UI")]
    public GameObject containerPanel;
    public GameObject notJobPanel;
    public GameObject afterJobPanel;

    private int previousFloor = 999;
    private Container selectedContainer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitContainers();

        if (containerPanel != null)
        {
            containerPanel.SetActive(false);
        }

        if (notJobPanel != null)
        {
            notJobPanel.SetActive(false);
        }

        if (afterJobPanel != null)
        {
            afterJobPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        int currentFloor = GameManager.Instance.GetCurrentFloor();

        if (currentFloor == 1)
        {
            if (previousFloor != currentFloor)
            {
                CloseContainerPanel();
                DisableAllFloorContainers();
                previousFloor = currentFloor;
            }

            return;
        }

        if (currentFloor != previousFloor)
        {
            OnFloorChanged(currentFloor);
            previousFloor = currentFloor;
        }
    }

    void InitContainers()
    {
        InitFloorContainers(floorB1Containers, -1);
        InitFloorContainers(floorB2Containers, -2);
        InitFloorContainers(floorB3Containers, -3);
        InitFloorContainers(floorB4Containers, -4);
    }

    void InitFloorContainers(List<Container> containers, int floor)
    {
        for (int i = 0; i < containers.Count; i++)
        {
            if (containers[i] == null) continue;

            containers[i].Init(floor, i);
        }
    }

    void OnFloorChanged(int currentFloor)
    {
        CloseContainerPanel();
        DisableAllFloorContainers();

        List<Container> currentFloorContainers = GetContainersByFloor(currentFloor);

        if (currentFloorContainers == null) return;

        EnableFirstFunctionToFloorContainers(currentFloorContainers);
    }

    List<Container> GetContainersByFloor(int floor)
    {
        switch (floor)
        {
            case -1:
                return floorB1Containers;

            case -2:
                return floorB2Containers;

            case -3:
                return floorB3Containers;

            case -4:
                return floorB4Containers;

            default:
                return null;
        }
    }

    void EnableFirstFunctionToFloorContainers(List<Container> containers)
    {
        foreach (Container container in containers)
        {
            if (container == null) continue;

            container.GiveFirstFunction();
        }
    }

    void DisableAllFloorContainers()
    {
        DisableFloorContainers(floorB1Containers);
        DisableFloorContainers(floorB2Containers);
        DisableFloorContainers(floorB3Containers);
        DisableFloorContainers(floorB4Containers);
    }

    void DisableFloorContainers(List<Container> containers)
    {
        foreach (Container container in containers)
        {
            if (container == null) continue;

            container.RemoveFirstFunction();
        }
    }

    public void OpenContainerPanel(Container container)
    {
        selectedContainer = container;

        if (containerPanel != null)
        {
            containerPanel.SetActive(true);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPlayerControl(false);
        }

        RefreshContainerPanel();

        if (container.hasJob)
        {
            container.ExecuteContainerWork();
        }

        Debug.Log(
            $"Container Panel 열림: B{Mathf.Abs(container.floor)}층 / " +
            $"{container.containerIndex}번 / {container.GetContainerTypeName()} / ID: {container.containerId}"
        );
    }

    public void RefreshContainerPanel()
    {
        if (selectedContainer == null) return;

        if (notJobPanel != null)
        {
            notJobPanel.SetActive(!selectedContainer.hasJob);
        }

        if (afterJobPanel != null)
        {
            afterJobPanel.SetActive(selectedContainer.hasJob);
        }
    }

    public void CloseContainerPanel()
    {
        selectedContainer = null;

        if (containerPanel != null)
        {
            containerPanel.SetActive(false);
        }

        if (notJobPanel != null)
        {
            notJobPanel.SetActive(false);
        }

        if (afterJobPanel != null)
        {
            afterJobPanel.SetActive(false);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPlayerControl(true);
        }

        Debug.Log("Container Panel 닫힘");
    }

    public Container GetSelectedContainer()
    {
        return selectedContainer;
    }

    public void SetSelectedContainerJobPowerPlant()
    {
        SetSelectedContainerJob("C_1_1");
    }

    public void SetSelectedContainerJobStorage()
    {
        SetSelectedContainerJob("C_2");
    }

    public void SetSelectedContainerJobAdvancedPowerPlant()
    {
        SetSelectedContainerJob("C_1_2");
    }

    public void SetSelectedContainerJobAquarium()
    {
        SetSelectedContainerJob("C_3");
    }

    public void SetSelectedContainerJob(string id)
    {
        if (selectedContainer == null) return;

        selectedContainer.SetJob(id);

        RefreshContainerPanel();

        selectedContainer.ExecuteContainerWork();
    }
}