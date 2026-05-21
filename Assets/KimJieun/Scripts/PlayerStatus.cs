using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Oxygen Settings")]
    public float maxOxygen = 100f;
    public float currentOxygen; 
    public float oxygenDecreaseRate = 5f;
    public float suffocateDamageRate = 20f;

    [Header("Armor Settings")]
    public bool hasWetsuit = false;
    public float baseDefense = 0f;
    public float wetsuitDefense = 30f;

    [Header("UI Connections")]
    public Image healthFill;
    public Image oxygenFill;
    public Image armorIndicator;

    private PlayerMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        currentHealth = maxHealth;
        currentOxygen = maxOxygen;

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.Sea)
        {
            DecreaseOxygen();
        }

        UpdateUI();
    }

    void DecreaseOxygen()
    {
        currentOxygen -= oxygenDecreaseRate * Time.deltaTime;
        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);

        if (currentOxygen <= 0f)
        {
            currentHealth -= suffocateDamageRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            if (currentHealth <= 0f)
            {
                Die();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        float defense = hasWetsuit ? wetsuitDefense : baseDefense;
        float actualDamage = Mathf.Max(0, damage - defense);

        currentHealth -= actualDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0f)
            {
                Die();
            }
    }

    public void SetMaxOxygen(float newMaxOxygen)
    {
        maxOxygen = newMaxOxygen;
        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);
    }

    void Die()
    {
        Debug.Log("Player has died.");
    }

    void UpdateUI()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }

        if (oxygenFill != null)
        {
            oxygenFill.fillAmount = currentOxygen / maxOxygen;
        }

        if (armorIndicator != null)
        {
            armorIndicator.gameObject.SetActive(hasWetsuit);
        }
    }
}