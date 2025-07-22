# 3D Ping Pong Game with Bare Sidecar: Unity Basics 101

This guide is for beginners new to Unity, explaining how to build a two-player 3D ping pong game (like squash) with a Bare-based sidecar for peer-to-peer (P2P) networking, inspired by [bare-expo](https://github.com/holepunchto/bare-expo). The game has two paddles, a ball, and a court, with players hitting the ball to score points (first to 5 wins). The Bare sidecar handles multiplayer networking, letting two players connect directly. This README covers Unity basics, setting up the game, testing it, creating a ZIP file, and pushing to GitHub.

## Unity Basics 101

Unity is a game development engine for creating 2D and 3D games, like this ping pong game. Here’s what you need to know as a beginner:

### What is Unity?
- **Unity Editor**: The app where you build your game, with windows to design and test.
- **GameObjects**: Objects in your game, like paddles, the ball, and walls. Think of them as Lego pieces.
- **Components**: Add-ons that give GameObjects features, like position (`Transform`), visuals (`MeshRenderer`), or code (`Scripts`).
- **Scenes**: A scene is like a game level. This game has one scene (`PingPongScene.unity`) with the court, paddles, and ball.
- **Scripts**: Code (in C#) that controls game behavior. `PingPongGame.cs` moves paddles, updates the ball, tracks scores, and talks to the sidecar.

### Key Unity Windows
- **Scene View**: Where you see and move 3D objects (e.g., place paddles).
- **Game View**: Shows what the player sees through the camera.
- **Hierarchy**: Lists all GameObjects in your scene (e.g., `PlayerPaddle`, `Ball`).
- **Inspector**: Shows details of a selected GameObject, like its position or attached script.
- **Project Window**: Stores your files (scripts, scenes) in `Assets/`.

### Core Concepts for the Ping Pong Game
- **GameObjects**:
  - **Paddles**: Cubes (`PlayerPaddle` at `(-5, 1, 0)`, `RemotePaddle` at `(5, 1, 0)`).
  - **Ball**: Sphere at `(0, 0, 0)`, moving with velocity.
  - **Court**: Plane (floor) and cubes (walls).
  - **GameManager**: Empty GameObject with `PingPongGame.cs` to control the game.
  - **Camera**: Shows the 3D scene from `(0, 5, -10)`.
- **Scripts** (`PingPongGame.cs`):
  - `Start()`: Sets up initial positions and starts the sidecar.
  - `Update()`: Runs every frame to move paddles (WASD or arrows), update the ball, and check collisions.
  - Sends/receives data (paddle positions, ball position/velocity, scores) to/from the sidecar via named pipes.
- **Input**: Uses WASD (Player 1) and arrow keys (Player 2) via Unity’s Input Manager.
- **Physics**: Simple math-based physics (not Unity’s physics engine) to move the ball and detect hits.
- **Networking**: The sidecar (`sidecar.js`) uses Bare’s `@hyperswarm/rpc` for P2P, sending data like `paddle:x,y,z` or `score:playerScore:remoteScore`.

### Tips for Beginners
- **Play Around**: Move objects in the Scene View, test in Game View with the Play button.
- **Check Console**: Look for errors or logs (e.g., “Connected to sidecar”) in the Console window.
- **Learn More**: Try Unity Learn[](https://learn.unity.com) for free tutorials.

## Project Structure
- `UnityProject/`: Unity game files.
  - `Assets/Scripts/PingPongGame.cs`: Game logic.
  - `Assets/Scenes/PingPongScene.unity`: Scene (create manually).
  - `ProjectSettings/InputManager.asset`: Input axes for Player 2 (configure manually).
- `Sidecar/`: Bare sidecar for P2P networking.
  - `sidecar.js`: Networking logic.
  - `package.json`: Node.js dependencies.
- `README.md`: This guide.
- `LICENSE`: MIT license.

## Requirements
- **Unity**: 2022.3 LTS (download via Unity Hub: https://unity.com/download)
- **Node.js**: v16 or later[](https://nodejs.org/en/download/)
- **Bare**: Holepunch Bare runtime[](https://github.com/holepunchto/bare)
- **pkg**: For compiling the sidecar (`npm install -g pkg`)
- **Git**: For GitHub[](https://git-scm.com/downloads)

## Setup Instructions

### 1. Set Up Unity
1. **Install Unity**:
   - Install Unity Hub from https://unity.com/download.
   - In Unity Hub, install Unity 2022.3 LTS.
   - Create a new 3D project: `PingPongGame/UnityProject`.

2. **Create Scene**:
   - In Unity, go to `File > New Scene`, save as `Assets/Scenes/PingPongScene.unity`.
   - Add GameObjects in the Hierarchy:
     - **Floor**: `GameObject > 3D Object > Plane`, position `(0, 0, 0)`, scale `(2, 1, 1)`.
     - **Top Wall**: `GameObject > 3D Object > Cube`, position `(0, 5, 0)`, scale `(12, 0.1, 2)`.
     - **Bottom Wall**: Cube, position `(0, 0, 0)`, scale `(12, 0.1, 2)`.
     - **Left Wall**: Cube, position `(-6, 2.5, 0)`, scale `(0.1, 5, 2)`.
     - **Right Wall**: Cube, position `(6, 2.5, 0)`, scale `(0.1, 5, 2)`.
     - **Player Paddle**: Cube, position `(-5, 1, 0)`, scale `(0.5, 1, 0.2)`, name `PlayerPaddle`.
     - **Remote Paddle**: Cube, position `(5, 1, 0)`, scale `(0.5, 1, 0.2)`, name `RemotePaddle`.
     - **Ball**: `GameObject > 3D Object > Sphere`, position `(0, 0, 0)`, scale `(0.2, 0.2, 0.2)`, name `Ball`.
     - **Main Camera**: Set position `(0, 5, -10)`, rotation `(30, 0, 0)` in the Inspector.
   - Create an empty GameObject: `GameObject > Create Empty`, name it `GameManager`.

3. **Add Script**:
   - In Project Window, create `Assets/Scripts/`.
   - Right-click in `Scripts/`, select `Create > C# Script`, name it `PingPongGame`.
   - Open `PingPongGame.cs`, paste the code from the provided artifact.
   - Drag `PingPongGame.cs` onto `GameManager` in the Hierarchy.
   - In the Inspector for `GameManager`, assign:
     - `Player Paddle`: Drag `PlayerPaddle` from Hierarchy.
     - `Remote Paddle`: Drag `RemotePaddle`.
     - `Ball`: Drag `Ball`.

4. **Configure Input**:
   - Go to `Edit > Project Settings > Input Manager`.
   - Expand `Axes`, click `+` to add:
     - **Horizontal2**: Name: `Horizontal2`, Positive Button: `right`, Negative Button: `left`, Type: Key or Mouse Button, Axis: X Axis.
     - **Vertical2**: Name: `Vertical2`, Positive Button: `up`, Negative Button: `down`, Type: Key or Mouse Button, Axis: Y Axis.
   - Save the project.

5. **Test in Editor**:
   - Click the Play button in Unity.
   - Use WASD to move `PlayerPaddle`, check if the ball moves and bounces off walls/paddles.
   - Note: Multiplayer requires the sidecar (see below).

### 2. Set Up Bare Sidecar
1. **Install Node.js**:
   - Download and install Node.js v16 or later: https://nodejs.org/en/download/.

2. **Install Bare**:
   ```bash
   git clone https://github.com/holepunchto/bare
   cd bare
   npm install
   npm run bare-make