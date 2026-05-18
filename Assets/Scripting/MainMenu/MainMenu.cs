using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject settingPanel;

    public void OnClickStart()
    {
        // Start 버튼 클릭 시 씬 이동
        SceneManager.LoadScene("Ground");
    }

    public void OnClickSetting()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
        }
    }

    public void OnClickExit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}