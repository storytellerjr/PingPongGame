PingPongGame/
├── UnityProject/
│   ├── Assets/
│   │   ├── Scenes/
│   │   │   └── PingPongScene.unity  (created manually in Unity)
│   │   └── Scripts/
│   │       └── PingPongGame.cs
│   └── ProjectSettings/
│       └── InputManager.asset  (configured manually)
├── Sidecar/
│   ├── sidecar.js
│   └── package.json
├── README.md
├── LICENSE
└── .gitignore


## Prerequisites

VS Code: Install from https://code.visualstudio.com.



Git: Install from https://git-scm.com/downloads. Verify with git --version in the terminal (should show a version, e.g., git version 2.39.2).



GitHub Account: Ensure you have an account with username storytellerjr. Check at https://github.com/storytellerjr.



Project Files: Have PingPongGame.cs, sidecar.js, package.json, README.md, and LICENSE saved from provided artifacts. Create PingPongScene.unity and configure InputManager.asset in Unity (per README.md).

## Instructions

# 1. Open Project in VS Code





Launch VS Code.



Go to File > Open Folder and select the PingPongGame folder.



Confirm it contains UnityProject/, Sidecar/, README.md, LICENSE, and .gitignore.

# 2. Verify or Create .gitignore





In VS Code’s Explorer (left sidebar, Ctrl+Shift+E or Cmd+Shift+E), check for PingPongGame/.gitignore.



If missing, create it:





Right-click in Explorer, select New File, name it .gitignore.



Paste:

# Unity
UnityProject/[Ll]ibrary/
UnityProject/[Tt]emp/
UnityProject/[Oo]bj/
UnityProject/[Bb]uild/
UnityProject/[Bb]uilds/
UnityProject/[Aa]ssets/Scenes/*.unity.meta
*.csproj
*.sln

# Sidecar
Sidecar/node_modules/
Sidecar/P2PSidecar
Sidecar/P2PSidecar.exe



Save (Ctrl+S or Cmd+S).



This excludes Unity build files and Node.js artifacts.

# 3. Open Terminal





Click Terminal > New Terminal (or press Ctrl+`` on Windows/Linux, Cmd+`` on macOS).



The terminal opens in PingPongGame (e.g., ~/PingPongGame$).

# 4. Check GitHub Username





Run:

git config user.name



This shows your Git username (e.g., storytellerjr or a display name).



Check your email:

git config user.email



If incorrect, set them:

git config --global user.name "storytellerjr"
git config --global user.email "your-email@example.com"





Use the email linked to your GitHub account (check at https://github.com/settings/emails).

# 5. Initialize Git Repository





Run:

git init



This creates a .git folder. If already initialized, it’ll say “Reinitialized existing Git repository.”

# 6. Stage Files





Run:

git add .



This stages all files (e.g., README.md, LICENSE, PingPongGame.cs, sidecar.js, package.json).



Note: If PingPongScene.unity or InputManager.asset aren’t included, create them in Unity (per README.md) and re-run git add ..



Check staged files:

git status

# 7. Add Commit





Run:

git commit -m "Initial commit of 3D ping pong game with Bare sidecar"



This commits all staged files with the specified message.



Verify:

git log





You’ll see the commit with your message, username, and date (around July 22, 2025, 4:56 PM AST).

# 8. Create GitHub Repository





Go to https://github.com/new in a browser.



Sign in with your GitHub account (username: storytellerjr).



Create a repository named PingPongGame (or your choice).



Set public or private, but do not initialize with a README, .gitignore, or license.



Copy the repository URL: https://github.com/storytellerjr/PingPongGame.git.

# 9. Add Remote and Push





In the terminal, add the remote:

git remote add origin https://github.com/storytellerjr/PingPongGame.git



Verify:

git remote -v





Should show:

origin  https://github.com/storytellerjr/PingPongGame.git (fetch)
origin  https://github.com/storytellerjr/PingPongGame.git (push)



Set the main branch:

git branch -M main



Push to GitHub:

git push -u origin main



If prompted, authenticate:





Enter your GitHub username (storytellerjr) and a personal access token:





Generate a token at https://github.com/settings/tokens (select repo scope).



Paste the token when prompted for a password.



Or, use the GitHub extension in VS Code:





Install via Ctrl+Shift+X, search “GitHub Pull Requests and Issues,” and sign in.

# 10. Verify on GitHub





Visit https://github.com/storytellerjr/PingPongGame.



Confirm files are present: README.md, LICENSE, UnityProject/Assets/Scripts/PingPongGame.cs, Sidecar/sidecar.js, Sidecar/package.json, etc.



If files are missing, check git status and ensure they were staged/committed.

Troubleshooting





No Username/Email:





If git config user.name is empty, set it (see step 4).



Authentication Error:





Use a personal access token (https://github.com/settings/tokens) if git push fails.



Or, sign in via the GitHub extension in VS Code.



Files Not Committed:





Run git status to check for untracked files.



Ensure files aren’t excluded by .gitignore.



Unity Files:





If PingPongScene.unity or InputManager.asset are missing, create them in Unity (per README.md) and re-run git add . and git commit.

Notes





Username: Use storytellerjr in the repository URL. Confirm at https://github.com/storytellerjr.



VS Code Terminal: These commands are run in the terminal, but you can also use VS Code’s Source Control view (Ctrl+Shift+G) to stage/commit (type the same message).



Next Steps: Test the game (per README.md) by setting up Unity, building the sidecar, and running two instances for multiplayer.