# Unity Ping Pong Game Setup

This directory contains the Unity project for a 3D ping pong game with P2P networking via a Bare sidecar.

## Prerequisites
- Unity 2022.3 LTS or later
- Node.js v16 or later (for building the sidecar)

## Setup Instructions
1. **Open Unity Project**:
   - Open Unity Hub, click "Add", and select the `UnityProject` folder.
   - Open the project in Unity 2022.3 or later.

2. **Create Scene**:
   - In Unity, create a new scene (`Assets/Scenes/PingPongScene.unity`).
   - Add GameObjects:
     - **Floor**: Create a Plane (`GameObject > 3D Object > Plane`), position `(0, 0, 0)`, scale `(2, 1, 1)`.
     - **Top Wall**: Create a Cube, position `(0, 5, 0)`, scale `(12, 0.1, 2)`.
     - **Bottom Wall**: Create a Cube, position `(0, 0, 0)`, scale `(12, 0.1, 2)`.
     - **Left Wall**: Create a Cube, position `(-6, 2.5, 0)`, scale `(0.1, 5, 2)`.
     - **Right Wall**: Create a Cube, position `(6, 2.5, 0)`, scale `(0.1, 5, 2)`.
     - **Player Paddle**: Create a Cube, position `(-5, 1, 0)`, scale `(0.5, 1, 0.2)`.
     - **Remote Paddle**: Create a Cube, position `(5, 1, 0)`, scale `(0.5, 1, 0.2)`.
     - **Ball**: Create a Sphere, position `(0, 0, 0)`, scale `(0.2, 0.2, 0.2)`.
     - **Main Camera**: Position `(0, 5, -10)`, rotation `(30, 0, 0)`.
   - Create an empty GameObject (`GameManager`), attach `PingPongGame.cs`.
   - In the Inspector, assign `PlayerPaddle`, `RemotePaddle`, and `Ball` to the script’s public fields.

3. **Configure Input**:
   - Go to `Edit > Project Settings > Input Manager`.
   - Add axes:
     - `Horizontal2`: Positive Button: `right`, Negative Button: `left`, Type: Key or Mouse Button.
     - `Vertical2`: Positive Button: `up`, Negative Button: `down`, Type: Key or Mouse Button.

4. **Build**:
   - Go to `File > Build Settings`, select your platform (Windows, macOS, or Linux).
   - Build to a folder (e.g., `build/`).
   - Copy the `P2PSidecar` executable from `Sidecar/` to the build folder’s root.

5. **Test Locally**:
   - Run two Unity instances (or build two executables).
   - In one instance, set `isPlayer1 = true` in `PingPongGame.cs`; in the other, set `isPlayer1 = false`.
   - Exchange Bare public keys (from sidecar logs) between instances.
   - Move paddles (WASD for Player 1, arrows for Player 2) and verify ball/score updates.

## Notes
- For internet-based P2P, implement a DHT for peer discovery (not included).
- Add a UI for scores and key entry in production.