using UnityEngine;

public class SceneMoveTrigger : MonoBehaviour
{
    [Header("Scene")]
    public string sceneName;

    [Header("Option")]
    public bool useGameManagerNextScene = false;

    private bool hasMoved = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasMoved) return;

        if (!other.CompareTag("Player")) return;

        hasMoved = true;

        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager가 없습니다.");
            return;
        }

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