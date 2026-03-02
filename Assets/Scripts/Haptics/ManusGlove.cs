using UnityEngine;
// using Manus; // uncomment real SDK

public class ManusGlove : HapticGlove
{
    // Manus-specific handles
    // ManusDevice manusDevice;

    void Awake()
    {
        enabledHardware = true;
        TryConnect();
    }

    void TryConnect()
    {
        try
        {
            // manusDevice = ManusSDK.Connect(gloveId);
            Debug.Log("[Manus] Connected (mock).");
        }
        catch
        {
            enabledHardware = false;
        }
    }

    public override void SendImpact(float intensity, float duration)
    {
        if (!enabledHardware) { base.SendImpact(intensity, duration); return; }
        // Manus API example:
        // manusDevice.Haptic.Vibrate(intensity, duration);
        Debug.Log($"[Manus] Haptic vibrate {intensity} for {duration}s");
    }

    public override void SetResistance(float strength)
    {
        if (!enabledHardware) { base.SetResistance(strength); return; }
        // manusDevice.Haptic.SetForce(strength);
        Debug.Log($"[Manus] Set resistance {strength}");
    }
}
