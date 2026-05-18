using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player;
    
    private Vector3 playerPosition;

    void Start()
    {
        //
    }

    void Update()
    {
        Vector3 pos = Player.transform.position; //매번 좌표를 계산하는 부분
    }

    int CurrentFloor(Vector3 pos)
    {
        float CurrentYfloor = pos.y;
        if (CurrentYfloor > 0.5)
        {
            return 1;
        }
        else if (CurrentYfloor > -1.24)
            return -1;
        else if (CurrentYfloor > -3.05)
            return -2;
        else if (CurrentYfloor > -4.6)
            return -3;
        else
            return -4;
    }

}
