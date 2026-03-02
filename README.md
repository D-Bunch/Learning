# BioArena - Prototype

Prototype starter for BioArena: full-body, biometric-driven arena gameplay.

## Overview
This Unity project contains modular scripts:
- PlayerController: movement and basic interactions
- BioManager: simulated heart-rate / vitals and real-device hook points
- HapticGlove: abstract API to send haptic events (implement SDK calls here)
- HealthSystem: maps vitals to in-game damage/conditions
- ArenaManager: match flow and simple elimination

## Requirements
- Unity 2020.3 LTS or 2021+ recommended
- (Optional) Mirror Networking package for multiplayer
- (Optional) Haptic SDKs: HaptX / Manus / SenseGlove (plug into HapticGlove.cs)

## How to use
1. Create a new Unity project and copy `Assets/` and `Scenes/` into your project.
2. Open `Main.unity` scene.
3. Add the `Player` prefab to the scene (or drag into Hierarchy).
4. Play in Editor. BioManager provides simulated vitals by default.

## Extending to real hardware
See `Docs/HardwareIntegration.md` for recommended SDKs and where to hook real data (BioManager -> HapticGlove).

## License
MIT-style for prototype code. Use, modify, and push to GitHub.
