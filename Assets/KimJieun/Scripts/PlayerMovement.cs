using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("SeaOrGroundMode")] 
    public bool Sea = false;
    public bool Ground = true;
    
    
    [Header("Movement Setting")]
    public bool canMoveHorizontal = true;
    public bool canMoveVertical = true;

    [Header("Move Speed")]
    public float moveSpeed = 5f;

    [Header("Hook State")]
    [SerializeField] public bool _isHookMode = false; //정혜교 땅 모드



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
        rb = GetComponent<Rigidbody2D>();
        sr = bodySr;
        hookSystem = GetComponent<HookSystem>();
        
        if (Sea == true)
        {
            isHookMode = false;
        }
        else {
            //sea가 false일때 실행할 코드
            rb.gravityScale = 9.8f;
            canMoveVertical = false;
            
            if (arm != null) arm.SetActive(false);
            if (hook != null) hook.SetActive(false);

        }

    }

    // Update is called once per frame
    void Update()
    {
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
            HandleFlipByKey();
                
        }

        else
        {
            //Sea가 false면 실행될 코드
            // 땅모드
            HandleMovement();
            HandleFlipByKey();

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
            // 땅모드에서는 위아래 이동 금지
            moveInput.y = 0f;
        }

        if (!canMoveHorizontal) moveInput.x = 0f;
        if (!canMoveVertical) moveInput.y = 0f;

        rb.linearVelocity = moveInput * moveSpeed;
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



}
