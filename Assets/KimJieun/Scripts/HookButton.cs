using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HookButton : MonoBehaviour
{
    [Header("Connections")]
    public PlayerMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHookButtonClick()
    {
        if (playerMovement == null) return;

        playerMovement._isHookMode = !playerMovement._isHookMode;

        if (!playerMovement._isHookMode)
        {
            HookSystem hookSystem = playerMovement.GetComponent<HookSystem>();
            if (hookSystem != null) hookSystem.CancelHook();
        }
    }
}
