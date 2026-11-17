using UnityEngine;
using UnityEngine.UI;

public class VitalsUI : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] private BioManager bioManager;
    [SerializeField] private HealthSystem health;

    [Header("Heart Rate")]
    [SerializeField] private Text hrText;
    [SerializeField] private Image hrBar;
    [SerializeField] private Color hrCalmColor = Color.green;
    [SerializeField] private Color hrStressedColor = Color.red;

    [Header("Health")]
    [SerializeField] private Text healthText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Color healthLowColor = Color.red;
    [SerializeField] private Color healthFullColor = Color.green;

    [Header("Animation")]
    [SerializeField, Min(0f)] private float smoothSpeed = 8f;

    private float hrFill, healthFill;
    private int lastHr = int.MinValue;
    private int lastHealth = int.MinValue;
    private int lastMaxHealth = int.MinValue;

    void Update()
    {
        UpdateHeartRate();
        UpdateHealth();
    }

    private void UpdateHeartRate()
    {
        if (bioManager == null) return;

        float hr = bioManager.CurrentHeartRate;

        // Only rebuild the string when the displayed number changes
        int rounded = Mathf.RoundToInt(hr);
        if (hrText != null && rounded != lastHr)
        {
            lastHr = rounded;
            hrText.text = $"HR: {rounded} bpm";
        }

        if (hrBar != null)
        {
            float target = Mathf.InverseLerp(bioManager.restingHeartRate, bioManager.maxHeartRate, hr);
            hrFill = Smooth(hrFill, target);
            hrBar.fillAmount = hrFill;
            hrBar.color = Color.Lerp(hrCalmColor, hrStressedColor, hrFill);
        }
    }

    private void UpdateHealth()
    {
        if (health == null) return;

        int current = Mathf.RoundToInt(health.health);
        int max = Mathf.RoundToInt(health.maxHealth);

        if (healthText != null && (current != lastHealth || max != lastMaxHealth))
        {
            lastHealth = current;
            lastMaxHealth = max;
            healthText.text = $"HP: {current}/{max}";
        }

        if (healthBar != null)
        {
            float target = health.maxHealth > 0f ? Mathf.Clamp01(health.health / health.maxHealth) : 0f;
            healthFill = Smooth(healthFill, target);
            healthBar.fillAmount = healthFill;
            healthBar.color = Color.Lerp(healthLowColor, healthFullColor, healthFill);
        }
    }

    // Frame-rate independent smoothing
    private float Smooth(float current, float target)
    {
        return Mathf.Lerp(current, target, 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime));
    }
}
