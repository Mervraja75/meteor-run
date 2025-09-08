using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 10;          // can set number here or in Inspector
    public int currentHealth;

    [Header("UI")]
    public TMP_Text healthText;
    public Slider healthBar;            // place to drag the Slider for the health status

    void Start()
    {
        // 1) health status
        if (maxHealth <= 0) maxHealth = 10;
        currentHealth = maxHealth;

        // 2) to configure the health status as it changes its values
        if (healthBar)
        {
            healthBar.minValue     = 0;
            healthBar.maxValue     = maxHealth;      //maximum value of the health
            healthBar.wholeNumbers = true;           //accepts whole numbers (can be changed in the inspector)
            healthBar.value        = currentHealth;  //shows the current health value as the game goes on
        }

        UpdateHealthUI();
        UpdateBarColor();
    }

    void UpdateHealthUI()
    {
        if (healthText)
            healthText.text = "Health: " + currentHealth; //show the Health text
    }

    void UpdateBarColor()
    {
        if (healthBar && healthBar.fillRect)
        {
            var img = healthBar.fillRect.GetComponent<Image>();
            if (img)
            {
                float t = (float)currentHealth / maxHealth;
                if (t > 0.6f)      img.color = Color.green;  //in a good state
                else if (t > 0.3f) img.color = Color.yellow; //in a moderate state
                else               img.color = Color.red;    //in a bad state
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);

        if (healthBar)
            healthBar.value = currentHealth;   // knob and fill will move right→left

        UpdateHealthUI();
        UpdateBarColor();

        if (currentHealth == 0 && GameManager.instance)
            GameManager.instance.GameOver();
    }
}