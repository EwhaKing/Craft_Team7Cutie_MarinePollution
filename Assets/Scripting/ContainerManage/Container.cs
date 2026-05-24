using UnityEngine;

public class Container : MonoBehaviour
{
    [Header("Container Info")]
    public int floor;
    public int containerIndex;

    private bool hasFirstFunction = false;
    private bool canClick = false;

    public void Init(int floor, int index)
    {
        this.floor = floor;
        this.containerIndex = index;
    }

    public void GiveFirstFunction()
    {
        if (hasFirstFunction) return;

        hasFirstFunction = true;
        canClick = true;

        Debug.Log($"B{Mathf.Abs(floor)}층 Container {containerIndex} 클릭 가능");
    }

    public void RemoveFirstFunction()
    {
        if (!hasFirstFunction) return;

        hasFirstFunction = false;
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
}