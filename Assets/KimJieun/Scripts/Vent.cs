using UnityEngine;
using System.Collections;

public class Vent : MonoBehaviour
{
    [Header("Time Settings")]
    public float activeDuration = 3f;   
    public float inactiveDuration = 1f; 

    [Header("Knockback Settings")]
    public float pushForce = 20f;
    public float knockbackTime = 0.5f;

    [HideInInspector] 
    public bool isEmitting = false;    

    private bool isKnockingBack = false;  

    void Start()
    {
        StartCoroutine(VentRoutine());
    }

    IEnumerator VentRoutine()
    {
        while (true)
        {
            isEmitting = true;
            yield return new WaitForSeconds(activeDuration);

            isEmitting = false;
            yield return new WaitForSeconds(inactiveDuration);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isEmitting && other.CompareTag("Player") && !isKnockingBack)
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                StartCoroutine(KnockbackRoutine(playerRb));
            }
        }
    }

    IEnumerator KnockbackRoutine(Rigidbody2D playerRb)
    {
        isKnockingBack = true;
        
        float timer = 0f;
        Vector2 pushDirection = transform.up;

        while (timer < knockbackTime)
        {
            if (playerRb == null) break;

            playerRb.linearVelocity = pushDirection * pushForce;

            timer += Time.deltaTime;
            yield return null;
        }

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
        }
        isKnockingBack = false;
    }
}