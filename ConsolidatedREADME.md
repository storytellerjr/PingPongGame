# 3D Ping Pong Game with Bare Sidecar

A two-player 3D ping pong game (like squash) built in Unity with a Bare-based sidecar for peer-to-peer (P2P) networking, inspired by [bare-expo](https://github.com/holepunchto/bare-expo). Players control paddles to hit a ball in a 3D court, with game state (paddle positions, ball position/velocity, scores) synchronized via P2P using the Bare JavaScript runtime.

## Project Structure
- `UnityProject/`: Unity game with paddles, ball, and court.
- `UnityProject/Assets/Scripts/PingPongGame.cs`: Main game logic.
- `UnityProject/Assets/Scenes/PingPongScene.unity`: Unity scene (create manually).
- `UnityProject/ProjectSettings/InputManager.asset`: Input axes for Player 2 (partial).
- `Sidecar/`: Bare sidecar for P2P networking.
- `Sidecar/sidecar.js`: P2P networking logic using `@hyperswarm/rpc`.
- `Sidecar/package.json`: Node.js dependencies and build scripts.
- `README.md`: This file.
- `LICENSE`: MIT license.

## Requirements
- **Unity**: 2022.3 LTS or later
- **Node.js**: v16 or later[](https://nodejs.org/en/download/)
- **Bare**: Holepunch Bare runtime[](https://github.com/holepunchto/bare)
- **pkg**: For compiling the sidecar to an executable (`npm install -g pkg`)
- **Git**: For version control and GitHub push

## Setup Instructions

### 1. Set Up Unity Project
1. **Create Unity Project**:
   - Open Unity Hub, create a new 3D project named `PingPongGame/UnityProject`.
   - Ensure Unity 2022.3 LTS or later is installed.

2. **Create Scene**:
   - In Unity, create a new scene: `Assets/Scenes/PingPongScene.unity`.
   - Add GameObjects:
     - **Floor**: Create a Plane (`GameObject > 3D Object > Plane`), position `(0, 0, 0)`, scale `(2, 1, 1)`.
     - **Top Wall**: Create a Cube, position `(0, 5, 0)`, scale `(12, 0.1, 2)`.
     - **Bottom Wall**: Create a Cube, position `(0, 0, 0)`, scale `(12, 0.1, 2)`.
     - **Left Wall**: Create a Cube, position `(-6, 2.5, 0)`, scale `(0.1, 5, 2)`.
     - **Right Wall**: Create a Cube, position `(6, 2.5, 0)`, scale `(0.1, 5, 2)`.
     - **Player Paddle**: Create a Cube, position `(-5, 1, 0)`, scale `(0.5, 1, 0.2)`, name `PlayerPaddle`.
     - **Remote Paddle**: Create a Cube, position `(5, 1, 0)`, scale `(0.5, 1, 0.2)`, name `RemotePaddle`.
     - **Ball**: Create a Sphere, position `(0, 0, 0)`, scale `(0.2, 0.2, 0.2)`, name `Ball`.
     - **Main Camera**: Position `(0, 5, -10)`, rotation `(30, 0, 0)`.
   - Create an empty GameObject named `GameManager`.
   - Attach `PingPongGame.cs` (from `UnityProject/Assets/Scripts/`) to `GameManager`.
   - In the Inspector, assign `PlayerPaddle`, `RemotePaddle`, and `Ball` to the script’s public fields.

3. **Configure Input**:
   - Go to `Edit > Project Settings > Input Manager`.
   - Add axes for Player 2 (for local testing):
     - `Horizontal2`: Positive Button: `right`, Negative Button: `left`, Type: Key or Mouse Button, Axis: X.
     - `Vertical2`: Positive Button: `up`, Negative Button: `down`, Type: Key or Mouse Button, Axis: Y.
   - Save the project.

4. **Build Unity Project**:
   - Go to `File > Build Settings`, select your platform (Windows, macOS, or Linux).
   - Build to a folder (e.g., `PingPongGame/build/`).
   - Later, copy the `P2PSidecar` executable to the build folder’s root.

### 2. Set Up Bare Sidecar
1. **Install Node.js**:
   - Download and install Node.js v16 or later from https://nodejs.org/en/download/.

2. **Install Bare**:
   ```bash
   git clone https://github.com/holepunchto/bare
   cd bare
   npm install
   npm run bare-make