using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public int score;
    public EndGameMenu endGameMenu;
    public int remainingBricks;
    public BrickGenerator brickGenerator;

    public bool gameStarted = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            if (!string.IsNullOrEmpty(PlayerNameInput.playerName))
            {
                gameStarted = true;
                PlayerNameInput.HideInputField();
                LaunchBall();
            }
            else
            {
                Debug.Log("Please enter a player name to start the game.");
            }
        }
    }

    public void AddScore(int value)
    {
        if (gameStarted)
        {
            score += value;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void SaveScore()
    {
        string filePath = Application.persistentDataPath + "/score.csv";
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            string playerName = PlayerNameInput.playerName;
            string dateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            writer.WriteLine($"{playerName},{score},{dateTime}");
        }
        Debug.Log("Score saved to " + filePath);
    }

    public void EndGame()
    {
        Debug.Log("EndGame called");
        SaveScore();
        ResetGame();
        endGameMenu.GoToHighScores();
    }

    public void BrickDestroyed()
    {
        remainingBricks--;
        if (remainingBricks <= 0)
        {
            RegenerateLevel();
        }
    }

    void RegenerateLevel()
    {
        Debug.Log("Regenerating level...");
        brickGenerator.GenerateBricks();
        remainingBricks = FindObjectsOfType<Brick>().Length;
    }

    public void ResetGame()
    {
        score = 0;
        UpdateScoreText();
        gameStarted = false;
        PlayerNameInput.ClearPlayerName(); // Effacer le nom du joueur
        PlayerNameInput.ShowInputField();
        if (brickGenerator != null)
        {
            brickGenerator.GenerateBricks();
            remainingBricks = FindObjectsOfType<Brick>().Length;
        }
        else
        {
            Debug.LogError("BrickGenerator is not assigned in GameManager.");
        }
    }

    void LaunchBall()
    {
        BallController ball = FindObjectOfType<BallController>();
        if (ball != null)
        {
            ball.LaunchBall();
        }
        else
        {
            Debug.LogError("BallController not found in the scene.");
        }
    }
}
