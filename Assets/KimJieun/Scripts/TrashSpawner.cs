using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TrashAreaSpawner : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private GameObject trashPrefab;
    
    [Tooltip("Trash Spawn Count")]
    [SerializeField] private int spawnCount = 5;

    [Header("Spawnable Trash IDs")]
    [SerializeField] private List<string> spawnableTrashIds = new List<string>();

    private SpriteRenderer areaRenderer;

    private void Awake()
    {
        areaRenderer = GetComponent<SpriteRenderer>();
        
        if (Application.isPlaying)
        {
            areaRenderer.enabled = false;
        }
    }

    private void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnTrashInArea();
        }
    }

    private void SpawnTrashInArea()
    {
        if (spawnableTrashIds.Count == 0 || trashPrefab == null) return;

        Bounds bounds = areaRenderer.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, transform.position.z);

        int randomIndex = Random.Range(0, spawnableTrashIds.Count);
        string targetId = spawnableTrashIds[randomIndex];
        Item selectedData = GetTrashDataById(targetId);

        if (selectedData != null)
        {
            GameObject spawnedObj = Instantiate(trashPrefab, spawnPosition, Quaternion.identity);
            spawnedObj.layer = LayerMask.NameToLayer("Trash");
            
            if (spawnedObj.TryGetComponent<DroppedTrash>(out var droppedTrash))
            {
                droppedTrash.Init(selectedData);
            }
        }
    }

    private Item GetTrashDataById(string id)
    {
        return id switch
        {
            "T001" => TrashList.Can,
            "T002" => TrashList.Net,
            "T003" => TrashList.Chair,
            "T004" => TrashList.Desk,
            "T005" => TrashList.Clothing,
            "T006" => TrashList.Tire,
            "T007" => TrashList.Bottle,
            "T008" => TrashList.Phone,
            "T009" => TrashList.Television,
            "T010" => TrashList.Bike,
            "T011" => TrashList.Fridge,
            "T012" => TrashList.Laptop,
            _ => null
        };
    }
}