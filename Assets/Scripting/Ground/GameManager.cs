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

    private Vector3 playerPosition;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        playerPosition = Player.transform.position;
        currentFloor = CalculateCurrentFloor(playerPosition);
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
}