using UnityEngine;
// using Haptx; // uncomment when HaptX SDK present

public class HaptXGlove : HapticGlove
{
    // Example SDK objects (replace with real ones)
    // private HxGloveDevice hxDevice;

    void Start()
    {
        enabledHardware = true;
        InitializeHaptX();
    }

    void InitializeHaptX()
    {
        try
        {
            // hxDevice = HxSdk.GetDevice("left"); // example
            Debug.Log("[HaptX] Initialized glove (mock).");
        }
        catch(System.Exception ex)
        {
            Debug.LogWarning("[HaptX] init failed: " + ex.Message);
            enabledHardware = false;
        }
    }

    public override void SendImpact(float intensity, float duration)
    {
        if (!enabledHardware)
        {
            base.SendImpact(intensity, duration);
            return;
        }

        // Map intensity 0..1 to SDK pulse
        // hxDevice.SendHapticPulse(intensity * 100, duration);
        Debug.Log($"[HaptX] Pulse => intensity:{intensity} dur:{duration}");
    }

    public override void SetResistance(float strength)
    {
        if (!enabledHardware) { base.SetResistance(strength); return; }

        // hxDevice.SetForceFeedback(strength);
        Debug.Log($"[HaptX] SetResistance => {strength}");
    }
}
