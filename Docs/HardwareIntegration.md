# Hardware Integration Notes

## Haptics
- HaptX, Manus, and SenseGlove SDKs provide C# Unity plugins. Implement their APIs inside `Assets/Scripts/HapticGlove.cs` or create derived classes.
- Typical APIs: Initialize, Connect, SendHapticPulse(intensity, duration), SetForceFeedback(level).

## Biometric Input
- Many heart-rate sensors provide BLE or native SDKs.
- On desktop, you can use WebSocket or a local UDP bridge to feed data into Unity (BioManager.deviceProvider). Example flow:
  Sensor -> Mobile app (BLE read) -> UDP -> Unity (UDPListener) -> BioManager.

## Low-latency rendering
- For high-fidelity graphics, consider cloud-rendering + streaming tech. Keep latency budgets <40ms for responsive haptics.

## Recommended order of integration
1. Implement glove stub (HapticGlove) to run with simulated feedback.
2. Integrate real glove SDK for impact & resistance.
3. Replace BioManager.simulate with a device provider (IBioDeviceProvider).
4. Add networking (Mirror) and test multi-client sessions.
