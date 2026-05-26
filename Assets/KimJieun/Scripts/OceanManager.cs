using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public static OceanManager Instance;

    [Header("Pollution Settings")]
    public int pollution = 100;

    [Header("Pollution Decrease Amount Settings")]
    public int fastDecreaseAmount = 10;
    public int slowDecreaseAmount = 3;

    [Header("Trash Spawn Settings")]
    public bool canSpawnTrash = true;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnTrashCollected()
    {
        if (pollution <= 0) return;

        int decreaseAmount = GetDecreaseAmount();
        pollution -= decreaseAmount;
        pollution = Mathf.Clamp(pollution, 0, 100);

        Debug.Log("현재 오염도 : " + pollution + "%");

        if (pollution <= 0)
        {
            GameClear();
        }
    }

    int GetDecreaseAmount()
    {
        if (pollution > 70)
            return fastDecreaseAmount;
        else if (pollution > 60)
            return slowDecreaseAmount;
        else if (pollution > 30)
            return fastDecreaseAmount;
        else if (pollution > 20)
            return slowDecreaseAmount;
        else
            return fastDecreaseAmount;
    }

    void GameClear()
    {
        Debug.Log("게임 클리어! 바다가 깨끗해졌습니다!");
        canSpawnTrash = false;
    }

    public void SetCanSpawnTrash(bool value)
    {
        canSpawnTrash = value;
        Debug.Log("쓰레기 스폰 여부 : " + canSpawnTrash);
    }
}