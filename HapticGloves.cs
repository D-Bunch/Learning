using UnityEngine;

public class HapticGlove : MonoBehaviour
{
    public string gloveId = "left";
    public bool enabledHardware = false;

    // Example simple vibration: (intensity 0..1, duration seconds)
    public virtual void SendImpact(float intensity, float duration)
    {
        if (enabledHardware)
        {
            // TODO: call actual glove SDK here (e.g., Manus, HaptX)
            // Example pseudo:
            // GloveSDK.SendHapticPulse(gloveId, intensity, duration);
        }
        else
        {
            // fallback: simple Unity feedback (camera shake or UI)
            Debug.Log($"[HAPTIC] {gloveId} impact intensity={intensity} duration={duration}");
        }
    }

    // Force/resistance simulation (for grabbing heavy objects)
    // strength 0..1
    public virtual void SetResistance(float strength)
    {
        if (enabledHardware)
        {
            // TODO: call glove SDK
        }
        else
        {
            Debug.Log($"[HAPTIC] {gloveId} set resistance {strength}");
        }
    }
}
