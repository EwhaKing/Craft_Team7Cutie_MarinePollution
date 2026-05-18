using UnityEngine;
using UnityEngine.InputSystem;

public class ElevatorMove : MonoBehaviour
{
    [Header("Floor Target Positions")]
    public Transform groundPoint;
    public Transform basement1Point;
    public Transform basement2Point;
    public Transform basement3Point;
    public Transform basement4Point;

    [Header("Move Setting")]
    public float moveSpeed = 3f;

    [Header("Elevator State")]
    public bool isPlayerInElevator = false;
    public bool isMoving = false;

    [Header("Ground Physics")]
    public Collider2D groundCollider;

    [Header("Underground Elevator Walls")]
    public Collider2D leftWallCollider;
    public Collider2D rightWallCollider;
    
    private int currentFloor;
    private int targetFloor;

    private Vector3 targetPosition;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        if (GameManager.Instance != null)
        {
            currentFloor = GameManager.Instance.GetCurrentFloor();

            if (currentFloor == 0)
            {
                currentFloor = 1;
            }

            targetFloor = currentFloor;
            targetPosition = GetFloorPosition(currentFloor);
        }

        SetUndergroundWalls(currentFloor != 1);
    }

    void Update()//매 순간 재생되는 함수
    {

        isPlayerInElevator = GameManager.Instance.IsPlayerInElevatorRange();//앨리베이터 범위안에 플레이어가 있는가?

        if (boxCollider != null)
        {
            boxCollider.enabled = isPlayerInElevator || isMoving;
        }

        if (isPlayerInElevator && !isMoving)
        {
            HandleInput();
        }

        if (isMoving)
        {
            MoveElevator();
        }
    }

    void HandleInput()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)//s를 누르면?
        {
            if (currentFloor > -4)
            {
                Debug.Log("Pressed S");

                targetFloor = GetDownFloor(currentFloor);
                targetPosition = GetFloorPosition(targetFloor);

                Debug.Log("currentFloor: " + currentFloor);
                Debug.Log("targetFloor: " + targetFloor);
                Debug.Log("targetPosition: " + targetPosition);

                StartElevatorMove();
            }
            else
            {
                Debug.Log("이미 가장 아래층입니다.");
            }
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            if (currentFloor < 1)
            {
                Debug.Log("Pressed W");

                targetFloor = GetUpFloor(currentFloor);
                targetPosition = GetFloorPosition(targetFloor);

                Debug.Log("currentFloor: " + currentFloor);
                Debug.Log("targetFloor: " + targetFloor);
                Debug.Log("targetPosition: " + targetPosition);

                StartElevatorMove();
            }
            else
            {
                Debug.Log("이미 가장 위층입니다.");
            }
        }
    }
    
    int GetDownFloor(int floor)
    {
        if (floor == 1) return -1;
        if (floor == -1) return -2;
        if (floor == -2) return -3;
        if (floor == -3) return -4;

        // 이상한 층이면 현재 층 유지
        return floor;
    }
    int GetUpFloor(int floor)
    {
        if (floor == -4) return -3;
        if (floor == -3) return -2;
        if (floor == -2) return -1;
        if (floor == -1) return 1;

        // 이상한 층이면 현재 층 유지
        return floor;
    }
    

    void StartElevatorMove()
    {
        isMoving = true;

        // 엘리베이터 움직이는 동안 땅 Collider 끄기
        if (groundCollider != null)
        {
            groundCollider.enabled = false;
        }

        Transform playerTransform = GameManager.Instance.GetPlayerTransform();
        Rigidbody2D playerRb = GameManager.Instance.GetPlayerRigidbody2D();

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.gravityScale = 0f;
        }

        if (playerTransform != null)
        {
            // true는 현재 월드 위치를 유지한 채 부모만 바꿈
            playerTransform.SetParent(transform, true);
        }
    }

    void MoveElevator()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;

            currentFloor = targetFloor;

            Transform playerTransform = GameManager.Instance.GetPlayerTransform();
            Rigidbody2D playerRb = GameManager.Instance.GetPlayerRigidbody2D();

            if (playerTransform != null)
            {
                playerTransform.SetParent(null, true);
            }

            if (currentFloor == 1)
            {
                // 지상층
                if (playerRb != null)
                {
                    playerRb.linearVelocity = Vector2.zero;
                    playerRb.gravityScale = 1f;
                }

                if (groundCollider != null)
                {
                    groundCollider.enabled = true;
                }

                // 지상층에서는 엘리베이터 벽 끄기
                SetUndergroundWalls(false);
            }
            else
            {
                // 지하층
                if (playerRb != null)
                {
                    playerRb.linearVelocity = Vector2.zero;
                    playerRb.gravityScale = 0f;
                }

                if (groundCollider != null)
                {
                    groundCollider.enabled = false;
                }

                // 지하층에서는 빠져나가지 못하게 벽 켜기
                SetUndergroundWalls(true);
            }

            Debug.Log("도착 층: " + currentFloor);
        }
    }

    Vector3 GetFloorPosition(int floor)
    {
        if (floor == 1)
        {
            return groundPoint.position;
        }
        else if (floor == -1)
        {
            return basement1Point.position;
        }
        else if (floor == -2)
        {
            return basement2Point.position;
        }
        else if (floor == -3)
        {
            return basement3Point.position;
        }
        else if (floor == -4)
        {
            return basement4Point.position;
        }

        return groundPoint.position;
    }
    
    void SetUndergroundWalls(bool active)
    {
        if (leftWallCollider != null)
        {
            leftWallCollider.enabled = active;
        }

        if (rightWallCollider != null)
        {
            rightWallCollider.enabled = active;
        }
    }
}