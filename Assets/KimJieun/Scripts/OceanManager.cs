using UnityEngine;
using UnityEngine.Tilemaps;

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

    [Header("Tilemap Settings")]
    public Tilemap targetTilemap;

    [Header("Ocean Rule Tiles")]
    public TileBase dirtyOceanTile;
    public TileBase normalOceanTile;
    public TileBase cleanOceanTile;

    private int previousStage = -1;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateOceanTiles();
    }

    public void OnTrashCollected()
    {
        if (pollution <= 0) return;

        int decreaseAmount = GetDecreaseAmount();
        pollution -= decreaseAmount;
        pollution = Mathf.Clamp(pollution, 0, 100);

        Debug.Log("현재 오염도 : " + pollution + "%");

        UpdateOceanTiles();

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

    void UpdateOceanTiles()
    {
        int currentStage = 2;
        if (pollution > 70) currentStage = 2;
        else if (pollution > 30) currentStage = 1;
        else currentStage = 0;

        if (currentStage == previousStage) return;
        previousStage = currentStage;

        TileBase tileToSet = dirtyOceanTile;
        if (currentStage == 1) tileToSet = normalOceanTile;
        else if (currentStage == 0) tileToSet = cleanOceanTile;

        BoundsInt bounds = targetTilemap.cellBounds;

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (targetTilemap.HasTile(pos))
            {
                targetTilemap.SetTile(pos, tileToSet);
            }
        }
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