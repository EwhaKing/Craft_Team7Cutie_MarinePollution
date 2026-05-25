using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour
{
    [Header("Bubble Prefab")]
    public GameObject bubblePrefab;
    public Transform spawnPoint;

    [Header("Spawn Settings")]
    public float spawnInterval = 0.15f;

    private Vent targetVent;
    private bool isSpawningLoop = false;

    void Start()
    {
        targetVent = GetComponent<Vent>();
    }

    void Update()
    {
        if (targetVent == null) return;

        if (targetVent.isEmitting && !isSpawningLoop)
        {
            StartCoroutine(SpawnLoop());
        }
    }

    IEnumerator SpawnLoop()
    {
        isSpawningLoop = true;

        while (targetVent != null && targetVent.isEmitting)
        {
            SpawnBubble();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawningLoop = false;
    }

    void SpawnBubble()
    {
        if (bubblePrefab == null) return;

        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
        
        GameObject bubble = Instantiate(bubblePrefab, pos, transform.rotation);
    }
}