# 3D Ping Pong Game with Bare Sidecar

A two-player 3D ping pong game (like squash) built in Unity with a Bare-based sidecar for P2P networking, inspired by [bare-expo](https://github.com/holepunchto/bare-expo).

## Project Structure
- `UnityProject/`: Unity game with paddles, ball, and court.
- `Sidecar/`: Bare sidecar for P2P networking using `@hyperswarm/rpc`.
- `README.md`: This file.
- `LICENSE`: MIT license.

## Requirements
- Unity 2022.3 LTS or later
- Node.js v16 or later
- Bare runtime[](https://github.com/holepunchto/bare)
- pkg (for compiling sidecar)

## Setup
1. **Unity Project**: Follow instructions in `UnityProject/README.md`.
2. **Sidecar**: Follow instructions in `Sidecar/README.md`.
3. **Build and Test**:
   - Build the Unity project to a folder (e.g., `build/`).
   - Copy the `P2PSidecar` executable to the build folder.
   - Run two instances, exchange Bare public keys, and test multiplayer.

## Pushing to GitHub
1. **Initialize Git Repository**:
   ```bash
   cd PingPongGame
   git init
   git add .
   git commit -m "Initial commit of 3D ping pong game with Bare sidecar"