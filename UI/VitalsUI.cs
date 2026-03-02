using UnityEngine;
using UnityEngine.UI;

public class VitalsUI : MonoBehaviour
{
    public BioManager bioManager;
    public HealthSystem health;
    public Text hrText;
    public Image hrBar;
    public Text healthText;
    public Image healthBar;

    void Update()
    {
        if (bioManager != null && hrText != null)
            hrText.text = $"HR: {bioManager.CurrentHeartRate:0} bpm";

        if (bioManager != null && hrBar != null)
            hrBar.fillAmount = Mathf.InverseLerp(bioManager.restingHeartRate, bioManager.maxHeartRate, bioManager.CurrentHeartRate);

        if (health != null && healthText != null)
            healthText.text = $"HP: {health.health:0}/{health.maxHealth:0}";

        if (healthBar != null)
            healthBar.fillAmount = health.health / health.maxHealth;
    }
}
