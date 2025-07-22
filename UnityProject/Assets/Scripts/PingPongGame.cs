using UnityEngine;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

public class PingPongGame : MonoBehaviour
{
    public GameObject playerPaddle; // Local player's paddle
    public GameObject remotePaddle; // Remote player's paddle
    public GameObject ball; // Ball GameObject
    public int playerScore = 0; // Local player's score
    public int remoteScore = 0; // Remote player's score
    private float paddleSpeed = 10f;
    private float ballSpeed = 15f;
    private Vector3 ballVelocity;
    private NamedPipeClientStream pipeClient;
    private Vector3 lastPaddlePosition;
    private bool isPlayer1; // Set to true for Player 1, false for Player 2 (for local testing)

    void Start()
    {
        // Initialize positions
        playerPaddle.transform.position = new Vector3(-5, 1, 0); // Left side
        remotePaddle.transform.position = new Vector3(5, 1, 0); // Right side
        ball.transform.position = Vector3.zero;
        ballVelocity = new Vector3(ballSpeed, 0, 0); // Start moving right
        lastPaddlePosition = playerPaddle.transform.position;

        // Start sidecar
        StartSidecar();

        // Connect to sidecar
        pipeClient = new NamedPipeClientStream(".", "UnityP2PPipe", PipeDirection.InOut);
        Task.Run(() => ConnectToSidecar());

        // For testing, assign Player 1 or 2 (in production, use UI or config)
        isPlayer1 = true; // Change to false for Player 2 in second instance
    }

    void Update()
    {
        // Move local paddle
        float moveX = isPlayer1 ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal2"); // WASD or Arrows
        float moveY = isPlayer1 ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical2");
        playerPaddle.transform.Translate(new Vector3(moveX, moveY, 0) * paddleSpeed * Time.deltaTime);

        // Clamp paddle to court bounds
        playerPaddle.transform.position = new Vector3(
            playerPaddle.transform.position.x,
            Mathf.Clamp(playerPaddle.transform.position.y, 0.5f, 4.5f),
            0
        );

        // Move ball
        ball.transform.position += ballVelocity * Time.deltaTime;

        // Ball collision with walls
        if (ball.transform.position.y >= 5 || ball.transform.position.y <= 0) // Top/bottom walls
        {
            ballVelocity.y = -ballVelocity.y;
        }
        if (ball.transform.position.x >= 6) // Right wall (Player 1 scores)
        {
            playerScore++;
            SendScoreUpdate();
            ResetBall();
        }
        if (ball.transform.position.x <= -6) // Left wall (Player 2 scores)
        {
            remoteScore++;
            SendScoreUpdate();
            ResetBall();
        }

        // Ball collision with paddles
        if (Vector3.Distance(ball.transform.position, playerPaddle.transform.position) < 1 ||
            Vector3.Distance(ball.transform.position, remotePaddle.transform.position) < 1)
        {
            ballVelocity.x = -ballVelocity.x; // Reverse ball direction
            SendBallUpdate();
        }

        // Send paddle position if changed
        if (Vector3.Distance(playerPaddle.transform.position, lastPaddlePosition) > 0.01f)
        {
            lastPaddlePosition = playerPaddle.transform.position;
            SendPaddlePosition();
        }

        // Check win condition
        if (playerScore >= 5 || remoteScore >= 5)
        {
            Debug.Log(playerScore >= 5 ? "Player 1 Wins!" : "Player 2 Wins!");
            // In production, show UI and stop game
        }
    }

    void StartSidecar()
    {
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "P2PSidecar", // Bare-compiled executable
            CreateNoWindow = true,
            UseShellExecute = false
        };
        System.Diagnostics.Process.Start(startInfo);
    }

    async Task ConnectToSidecar()
    {
        try
        {
            await pipeClient.ConnectAsync();
            Debug.Log("Connected to sidecar");

            // Listen for sidecar messages
            byte[] buffer = new byte[1024];
            while (pipeClient.IsConnected)
            {
                int bytesRead = await pipeClient.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    ProcessSidecarMessage(message);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Sidecar connection error: {e.Message}");
        }
    }

    void SendPaddlePosition()
    {
        if (pipeClient.IsConnected)
        {
            string message = $"paddle:{playerPaddle.transform.position.x},{playerPaddle.transform.position.y},{playerPaddle.transform.position.z}";
            byte[] data = Encoding.UTF8.GetBytes(message);
            pipeClient.Write(data, 0, data.Length);
            pipeClient.Flush();
        }
    }

    void SendBallUpdate()
    {
        if (pipeClient.IsConnected)
        {
            string message = $"ball:{ball.transform.position.x},{ball.transform.position.y},{ball.transform.position.z}:{ballVelocity.x},{ballVelocity.y},{ballVelocity.z}";
            byte[] data = Encoding.UTF8.GetBytes(message);
            pipeClient.Write(data, 0, data.Length);
            pipeClient.Flush();
        }
    }

    void SendScoreUpdate()
    {
        if (pipeClient.IsConnected)
        {
            string message = $"score:{playerScore}:{remoteScore}";
            byte[] data = Encoding.UTF8.GetBytes(message);
            pipeClient.Write(data, 0, data.Length);
            pipeClient.Flush();
        }
    }

    void ProcessSidecarMessage(string message)
    {
        string[] parts = message.Split(':');
        if (parts[0] == "paddle")
        {
            string[] pos = parts[1].Split(',');
            if (pos.Length == 3 && float.TryParse(pos[0], out float x) && float.TryParse(pos[1], out float y) && float.TryParse(pos[2], out float z))
            {
                remotePaddle.transform.position = new Vector3(x, y, z);
            }
        }
        else if (parts[0] == "ball")
        {
            string[] posVel = parts[1].Split(':');
            string[] pos = posVel[0].Split(',');
            string[] vel = posVel[1].Split(',');
            if (pos.Length == 3 && vel.Length == 3 &&
                float.TryParse(pos[0], out float x) && float.TryParse(pos[1], out float y) && float.TryParse(pos[2], out float z) &&
                float.TryParse(vel[0], out float vx) && float.TryParse(vel[1], out float vy) && float.TryParse(vel[2], out float vz))
            {
                ball.transform.position = new Vector3(x, y, z);
                ballVelocity = new Vector3(vx, vy, vz);
            }
        }
        else if (parts[0] == "score")
        {
            string[] scores = parts[1].Split(':');
            if (scores.Length == 2 && int.TryParse(scores[0], out int pScore) && int.TryParse(scores[1], out int rScore))
            {
                playerScore = pScore;
                remoteScore = rScore;
            }
        }
    }

    void ResetBall()
    {
        ball.transform.position = Vector3.zero;
        ballVelocity = new Vector3(ballSpeed * (isPlayer1 ? 1 : -1), 0, 0); // Start towards opponent
        SendBallUpdate();
    }

    void OnDestroy()
    {
        pipeClient?.Close();
        foreach (var process in System.Diagnostics.Process.GetProcessesByName("P2PSidecar"))
        {
            process.Kill();
        }
    }
}