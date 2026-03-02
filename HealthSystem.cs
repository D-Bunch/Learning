using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public bool isAlive => health > 0f;

    // How vitals modify damage: higher stress => more damage received multiplier
    public void TakeDamage(float baseDamage, BioManager bioManager = null)
    {
        float multiplier = 1f;
        if (bioManager != null)
        {
            float stress = bioManager.StressLevel; // 0..1
            multiplier = Mathf.Lerp(1f, 1.6f, stress); // up to +60% damage when stressed
        }
        float final = baseDamage * multiplier;
        health -= final;
        health = Mathf.Max(0, health);
        Debug.Log($"{name} took {final:0.0} damage (mult {multiplier:0.00}). Health now {health:0.0}.");
        if (!isAlive) OnEliminated();
    }

    void OnEliminated()
    {
        Debug.Log($"{name} eliminated.");
        // TODO: death animation, ragdoll, notify ArenaManager
    }
}
