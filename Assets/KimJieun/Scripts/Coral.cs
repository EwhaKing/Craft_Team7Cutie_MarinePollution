using UnityEngine;

public class Coral : MonoBehaviour
{
    [Header("Settings")]
    public int pollutionThreshold = 10;

    private OceanManager oceanManager;

    void Start()
    {
        oceanManager = FindFirstObjectByType<OceanManager>();
        if (oceanManager == null)
        {
            Debug.LogError("씬 내에서 OceanManager를 찾을 수 없습니다", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oceanManager != null)
        {
            // OceanManager의 pollution 값이 기준치 이하인지 체크
            if (oceanManager.pollution <= pollutionThreshold)
            {
                Destroy(gameObject); // 오브젝트 삭제
            }
        }
    }
}
