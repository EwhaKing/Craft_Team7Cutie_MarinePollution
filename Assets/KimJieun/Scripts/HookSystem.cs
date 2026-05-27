using UnityEngine;
using UnityEngine.InputSystem;

public class HookSystem : MonoBehaviour
{
    [Header("Hook Setting")]
    public float hookRange = 7f;
    public float hookSpeed = 15f;
    public float pullSpeed = 5f;

    [Header("Object Setting")]
    public Transform armPivot;
    public GameObject hook;
    public LineRenderer ropeRenderer;

    [Header("Layer Setting")]
    public LayerMask trashLayer;

    public enum HookState
    {
        Idle,
        Flying,
        Pulling,
        Returning,
    }

    public HookState currentState = HookState.Idle;
    private Vector2 targetPos;
    private Vector2 returnPos;
    private GameObject caughtTrash;
    private PlayerMovement playerMovement;

    private InventorySystem inventorySystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        inventorySystem = FindFirstObjectByType<InventorySystem>();

        if (inventorySystem == null)
        {
            Debug.LogError("씬 내에서 InventorySystem을 찾을 수 없습니다! Canvas의 인벤토리 UI에 스크립트가 잘 붙어있는지 확인하세요.", this);
        }

        hook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerMovement._isHookMode) return;

        if (Mouse.current.leftButton.wasPressedThisFrame && currentState == HookState.Idle)
        {
            TryShootHook();
        }

        switch (currentState)
        {
            case HookState.Flying:
                FlyHook();
                break;
            case HookState.Pulling:
                PullTrash();
                break;
            case HookState.Returning:
                ReturnHookToArm();
                break;
        }

        UpdateRope();
    }

    void TryShootHook()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        float distance = Vector2.Distance(transform.position, mousePos);

        if (distance > hookRange)
        {
            Debug.Log("너무 멀어서 끌고올 수 없습니다!");
            return;
        }

        ShootHook(mousePos);
    }

    void ShootHook(Vector2 target)
    {
        targetPos = target;

        hook.SetActive(true);
        hook.transform.position = armPivot.position;

        currentState = HookState.Flying;
    }

    void FlyHook()
    {
        hook.transform.position = Vector2.MoveTowards(
            hook.transform.position,
            targetPos,
            hookSpeed * Time.deltaTime
        );

        Collider2D hit = Physics2D.OverlapCircle(hook.transform.position, 0.3f, trashLayer);
        if (hit != null)
        {
            caughtTrash = hit.gameObject;
            currentState = HookState.Pulling;
            return;
        }

        if (Vector2.Distance(hook.transform.position, targetPos) < 0.1f)
        {
            returnPos = armPivot.position;
            currentState = HookState.Returning;
        }
    }

    void PullTrash()
    {
        if (caughtTrash == null)
        {
            ReturnHook();
            return;
        }

        caughtTrash.transform.position = Vector2.MoveTowards(
            caughtTrash.transform.position,
            transform.position,
            pullSpeed * Time.deltaTime
        );

        hook.transform.position = caughtTrash.transform.position;

        if (Vector2.Distance(caughtTrash.transform.position, transform.position) < 0.5f)
        {
            CollectTrash();
        }
        
    }

    void CollectTrash()
    {
        DroppedTrash droppedTrash = caughtTrash.GetComponent<DroppedTrash>();

        if (droppedTrash != null && inventorySystem != null)
        {
            if (droppedTrash.TrashData != null)
            {
                string targetId = "";

                if (droppedTrash.TrashData is TrashItem trash)
                {
                    targetId = trash.Id;
                }

                bool success = inventorySystem.AddItemById(targetId);

                if (!success)
                {
                    Debug.Log("인벤토리가 가득 찼습니다!");
                }
            }
        }

        if (OceanManager.Instance != null)
        {
            OceanManager.Instance.OnTrashCollected();
        }

        caughtTrash.SetActive(false);
        caughtTrash = null;

        ReturnHook();
    }

    void ReturnHookToArm()
    {
        hook.transform.position = Vector2.MoveTowards(
            hook.transform.position,
            returnPos,
            hookSpeed * Time.deltaTime
        );

        if (Vector2.Distance(hook.transform.position, returnPos) < 0.1f)
        {
            ReturnHook();
        }
    }

    void ReturnHook()
    {
        hook.SetActive(false);
        currentState = HookState.Idle;
    }

    void UpdateRope()
    {
        if (ropeRenderer == null) return;
        
        if (currentState == HookState.Idle)
        {
            ropeRenderer.enabled = false;
            return;
        }

        ropeRenderer.enabled = true;
        ropeRenderer.SetPosition(0, armPivot.position);
        ropeRenderer.SetPosition(1, hook.transform.position);
    }

    public void CancelHook()
    {
        if (caughtTrash != null)
        {
            caughtTrash = null;
        }
        ReturnHook();
    }
}
