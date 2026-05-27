using UnityEngine;

public class SceneMoveTrigger : MonoBehaviour
{
    [Header("Scene")]
    public string sceneName;

    [Header("Option")]
    public bool useGameManagerNextScene = false;

    private bool hasMoved = false;

    private void Start()
    {
        Debug.Log("SceneMoveTrigger 시작됨: " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D 호출됨. 들어온 오브젝트: " + other.name);

        if (hasMoved) return;

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Player 태그가 아님. 현재 태그: " + other.tag);
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager가 없습니다.");
            return;
        }

        hasMoved = true;

        if (useGameManagerNextScene)
        {
            GameManager.Instance.MoveToNextScene();
        }
        else
        {
            GameManager.Instance.MoveToScene(sceneName);
        }
    }
}