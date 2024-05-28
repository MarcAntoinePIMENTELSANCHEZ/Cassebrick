using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public GameObject endGameMenuUI;

    public void ShowEndGameMenu()
    {
        if (endGameMenuUI != null)
        {
            endGameMenuUI.SetActive(true);
            Time.timeScale = 0f; // Pause le jeu
        }
        else
        {
            Debug.LogError("EndGameMenuUI is not assigned in EndGameMenu script.");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Relance le jeu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Relance le jeu
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToHighScores()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ScoreScene"); // Charge la scène des meilleurs scores
    }
}
