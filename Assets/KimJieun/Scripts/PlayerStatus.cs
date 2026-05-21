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

    [Header("Armor Settings")]
    public bool hasWetsuit = false;
    public float baseDefense = 0f;
    public float wetsuitDefense = 30f;

    [Header("UI Connections")]
    public Image healthBar;
    public Image oxygenBar;
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
    }

    public void TakeDamage(float damage)
    {
        float defense = hasWetsuit ? wetsuitDefense : baseDefense;
        float actualDamage = Mathf.Max(0, damage - defense);

        currentHealth -= actualDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    public void SetMaxOxygen(float newMaxOxygen)
    {
        maxOxygen = newMaxOxygen;
        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);
    }

    void UpdateUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }

        if (oxygenBar != null)
        {
            oxygenBar.fillAmount = currentOxygen / maxOxygen;
        }

        if (armorIndicator != null)
        {
            armorIndicator.gameObject.SetActive(hasWetsuit);
        }
    }
}