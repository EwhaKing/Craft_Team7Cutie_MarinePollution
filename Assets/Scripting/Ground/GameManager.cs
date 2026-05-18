using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    public GameObject Player;

    [Header("Current Floor")]
    public int currentFloor;

    [Header("Elevator Range")]
    public float elevatorMinX = -1.89f;
    public float elevatorMaxX = 0.24f;

    [Header("Camera")]
    public Camera mainCamera;
    public float cameraFixedX = 0f;
    public float bottomFollowViewportY = 0.33f;
    public float topFollowViewportY = 0.66f;
    public bool clampPlayerXInCamera = true;
    public float cameraMoveSpeed = 5f;
    
    
    private Vector3 playerPosition;
    private Vector3 initialCameraPosition;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        
        if (mainCamera != null)
        {
            initialCameraPosition = mainCamera.transform.position;
        }
    }

    void Update()
    {
        if (Player == null) return;

        playerPosition = Player.transform.position;
        currentFloor = CalculateCurrentFloor(playerPosition);
    }

    void LateUpdate()
    {
        HandleCameraFollow();
        ClampPlayerXInsideCamera();
    }

    int CalculateCurrentFloor(Vector3 pos)
    {
        float currentYfloor = pos.y;

        if (currentYfloor > 0.5f)
        {
            return 1;
        }
        else if (currentYfloor > -1.24f)
        {
            return -1;
        }
        else if (currentYfloor > -3.05f)
        {
            return -2;
        }
        else if (currentYfloor > -4.6f)
        {
            return -3;
        }
        else
        {
            return -4;
        }
    }

    public int GetCurrentFloor()
    {
        return currentFloor;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    public bool IsPlayerInElevatorRange()
    {
        float playerX = playerPosition.x;

        return playerX >= elevatorMinX && playerX <= elevatorMaxX;
    }

    public Transform GetPlayerTransform()
    {
        if (Player == null) return null;
        return Player.transform;
    }

    public Rigidbody2D GetPlayerRigidbody2D()
    {
        if (Player == null) return null;
        return Player.GetComponent<Rigidbody2D>();
    }

    void HandleCameraFollow()
    {
        if (Player == null || mainCamera == null) return;

        Vector3 cameraPos = mainCamera.transform.position;
        Vector3 targetCameraPos = cameraPos;

        targetCameraPos.x = cameraFixedX;

        // 1층이면 초기 카메라 위치를 목표로 삼음
        if (currentFloor == 1)
        {
            targetCameraPos.y = initialCameraPosition.y;
            targetCameraPos.z = initialCameraPosition.z;
        }
        else
        {
            Vector3 playerViewportPos = mainCamera.WorldToViewportPoint(Player.transform.position);

            float cameraHeight = mainCamera.orthographicSize * 2f;

            if (playerViewportPos.y < bottomFollowViewportY)
            {
                float targetCameraY = Player.transform.position.y
                                      + mainCamera.orthographicSize
                                      - cameraHeight * bottomFollowViewportY;

                targetCameraPos.y = targetCameraY;
            }
            else if (playerViewportPos.y > topFollowViewportY)
            {
                float targetCameraY = Player.transform.position.y
                                      + mainCamera.orthographicSize
                                      - cameraHeight * topFollowViewportY;

                targetCameraPos.y = targetCameraY;
            }
        }

        mainCamera.transform.position = Vector3.Lerp(
            cameraPos,
            targetCameraPos,
            cameraMoveSpeed * Time.deltaTime
        );
    }

    void ClampPlayerXInsideCamera()
    {
        if (!clampPlayerXInCamera) return;
        if (Player == null || mainCamera == null) return;

        float cameraHalfHeight = mainCamera.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;

        float minX = mainCamera.transform.position.x - cameraHalfWidth;
        float maxX = mainCamera.transform.position.x + cameraHalfWidth;

        Vector3 playerPos = Player.transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, minX, maxX);

        Player.transform.position = playerPos;
    }
}