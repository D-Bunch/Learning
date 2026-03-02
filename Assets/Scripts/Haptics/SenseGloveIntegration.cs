using UnityEngine;
// using SenseGlove; // replace with actual SDK namespace

public class SenseGloveIntegration : HapticGlove
{
    // SenseGloveDevice sgDevice;

    void Start()
    {
        enabledHardware = true;
        InitSenseGlove();
    }

    void InitSenseGlove()
    {
        try
        {
            // sgDevice = SenseGloveSDK.Find(gloveId);
            Debug.Log("[SenseGlove] init mock OK");
        }
        catch
        {
            enabledHardware = false;
        }
    }

    public override void SendImpact(float intensity, float duration)
    {
        if (!enabledHardware) { base.SendImpact(intensity, duration); return; }
        // sgDevice.Haptics.SimplePulse(intensity, duration);
        Debug.Log($"[SenseGlove] Pulse {intensity} dur {duration}");
    }

    public override void SetResistance(float strength)
    {
        if (!enabledHardware) { base.SetResistance(strength); return; }
        // sgDevice.ApplyForceCurve(strength);
        Debug.Log($"[SenseGlove] Resistance {strength}");
    }
}
