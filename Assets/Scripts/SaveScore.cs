using UnityEngine;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    public Button endGameButton;

    void Start()
    {
        endGameButton.onClick.AddListener(EndTheGame);
    }

    void EndTheGame()
    {
        GameManager.instance.SaveScore();
        // Autres actions pour terminer le jeu
    }
}
