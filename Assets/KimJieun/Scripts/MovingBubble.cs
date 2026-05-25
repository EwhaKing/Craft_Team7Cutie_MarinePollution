using UnityEngine;

public class MovingBubble : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float lifetime = 2.5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            rb.linearVelocity = transform.up * speed; 
        }
        Destroy(gameObject, lifetime);
    }
}