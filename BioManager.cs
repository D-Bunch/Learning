using UnityEngine;
using System;

public class BioManager : MonoBehaviour
{
    [Header("Simulated HR settings")]
    public float restingHeartRate = 60f;
    public float maxHeartRate = 190f;
    public float CurrentHeartRate { get; private set; }
    public float StressLevel { get; private set; } // 0..1

    [Header("Simulation")]
    public bool simulate = true;
    public float hrNoise = 1.5f;

    // Real device hook (assign implementation)
    public IBioDeviceProvider deviceProvider;

    void Start()
    {
        CurrentHeartRate = restingHeartRate;
        StressLevel = 0f;
    }

    void Update()
    {
        if (simulate || deviceProvider == null)
            SimulateHeartRate();
        else
            ReadFromDevice();

        // Compute stress level as normalized HR elevation
        StressLevel = Mathf.InverseLerp(restingHeartRate, maxHeartRate, CurrentHeartRate);
    }

    void SimulateHeartRate()
    {
        // Basic simulation: walk between resting and moderate HR based on time & user input
        float target = restingHeartRate;
        if (Input.GetKey(KeyCode.LeftShift)) target = Mathf.Min(maxHeartRate, restingHeartRate + 60f); // sprint
        if (Input.GetKey(KeyCode.Space)) target += 20f; // adrenaline event
        CurrentHeartRate = Mathf.Lerp(CurrentHeartRate, target + UnityEngine.Random.Range(-hrNoise, hrNoise), Time.deltaTime * 1.5f);
    }

    void ReadFromDevice()
    {
        try
        {
            CurrentHeartRate = deviceProvider.GetHeartRate();
        }
        catch(Exception ex)
        {
            Debug.LogWarning("Bio device read exception: " + ex.Message);
            CurrentHeartRate = restingHeartRate;
        }
    }
}

// Interface - implement this for real hardware providers
public interface IBioDeviceProvider
{
    float GetHeartRate();
}
