using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private string currentAnim;
    
    [Header("SeaOrGroundMode")] 
    public bool Sea = false;
    public bool Ground = true;
    private Vector2 lastMoveDir = Vector2.right;
    
    [Header("Movement Setting")]
    public bool canMoveHorizontal = true;
    public bool canMoveVertical = true;

    [Header("Move Speed")]
    public float moveSpeed = 5f;

    [Header("Hook State")]
    [SerializeField] public bool _isHookMode = false; //정혜교 땅 모드

    
    [Header("Equipment")]
    public AttachedEquippmentManager equipmentManager;


    public bool isHookMode
    {
        get {return _isHookMode;}
        set
        {
            if (!Sea) return; //땅 모드랑 구별하려고 작성했습니다.
            
            _isHookMode = value;
            if (arm != null) arm.SetActive(_isHookMode);
            if (hook != null) hook.SetActive(_isHookMode);
        }
    }

    [Header("Object Setting")]
    public SpriteRenderer bodySr;
    public Transform armPivot;
    public GameObject arm;
    public GameObject hook;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private HookSystem hookSystem;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = bodySr != null ? bodySr : GetComponentInChildren<SpriteRenderer>();
        hookSystem = GetComponent<HookSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.CanPlayerControl())
        {
            return;
        }
        
        if (Sea)
        {
            rb.gravityScale = 0f;
            canMoveVertical = true;
            canMoveHorizontal = true;
            
            if (_isHookMode && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                _isHookMode = false;
                if (hookSystem != null) hookSystem.CancelHook();
            }

            arm.SetActive(_isHookMode);
            hook.SetActive(_isHookMode);

            if (_isHookMode)
            {
                rb.linearVelocity = Vector2.zero;
                HandleFlipByMouse();
                RotateArm();
                return;
            }

            HandleMovement();

                
        }

        else
        {
            //Sea가 false면 실행될 코드
            // 땅모드
            HandleMovement();


            if (arm != null) arm.SetActive(false);
            if (hook != null) hook.SetActive(false);
        }
    }

    void HandleMovement()
    {
        Vector2 moveInput = Vector2.zero;

        if (Keyboard.current.aKey.isPressed)
            moveInput.x = -1f;
        else if (Keyboard.current.dKey.isPressed)
            moveInput.x = 1f;

        if (Sea)
        {
            if (Keyboard.current.wKey.isPressed)
                moveInput.y = 1f;
            else if (Keyboard.current.sKey.isPressed)
                moveInput.y = -1f;
        }
        else
        {
            moveInput.y = 0f;
        }

        if (!canMoveHorizontal) moveInput.x = 0f;
        if (!canMoveVertical) moveInput.y = 0f;

        moveInput = moveInput.normalized;

        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDir = moveInput;
        }

        rb.linearVelocity = moveInput * moveSpeed;

        HandleAnimation(moveInput);
    }

    void HandleFlipByKey()
    {
        if (Keyboard.current.dKey.isPressed)
                sr.flipX = false;
        else if (Keyboard.current.aKey.isPressed)
                sr.flipX = true;
        

    }

    void HandleFlipByMouse()
    {
        
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (mousePos.x < transform.position.x)
                sr.flipX = true;
            else
                sr.flipX = false;
        

    }

    void RotateArm()
    {
        if (Sea)
        {
            if (armPivot == null) return;

            if (hookSystem != null && hookSystem.currentState == HookSystem.HookState.Returning)
            {
                return;
            }

            if (hookSystem != null && hookSystem.currentState == HookSystem.HookState.Flying)
            {
                return;
            }
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = mousePos - (Vector2)armPivot.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            armPivot.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            //Sea가 False일 때
        }
    }

    void PlayAnim(string animName)
    {
        if (animator == null) return;
        if (currentAnim == animName) return;

        animator.Play(animName, 0, 0f);
        currentAnim = animName;

        Debug.Log("PlayAnim 호출됨: " + animName);
    }
    
    void HandleAnimation(Vector2 moveInput)
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        bool hasWetSuit = equipmentManager != null && equipmentManager.HasWetSuit();

        if (!isMoving)
            return;

        Vector2 dir = moveInput;

        string animName;

        if (Sea)
        {
            if (hasWetSuit)
            {
                if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
                    animName = dir.x > 0 ? "Swim_Right_Suit" : "Swim_left_Suit";
                else
                    animName = dir.y > 0 ? "Swim_Up_Suit" : "Swim_Down_Suit";
            }
            else
            {
                if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
                    animName = dir.x > 0 ? "Swim_R_No" : "Swim_L_No";
                else
                    animName = dir.y > 0 ? "Swimming_Up_No" : "Swimming_Down_No";
            }
        }
        else
        {
            animName = dir.x >= 0 ? "Walk_R_NoSuit" : "Walk_L_NoSuit";
        }

        Debug.Log($"moveInput: {moveInput}, animName: {animName}, currentAnim: {currentAnim}");

        PlayAnim(animName);
    }

}
