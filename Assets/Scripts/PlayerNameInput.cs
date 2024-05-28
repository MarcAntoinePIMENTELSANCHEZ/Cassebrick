using UnityEngine;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public static string playerName;

    public void SetPlayerName()
    {
        playerName = nameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            GameManager.instance.gameStarted = true;
            HideInputField();
        }
    }

    public static void HideInputField()
    {
        PlayerNameInput instance = FindObjectOfType<PlayerNameInput>();
        if (instance != null && instance.nameInputField != null)
        {
            instance.nameInputField.gameObject.SetActive(false);
        }
    }

    public static void ShowInputField()
    {
        PlayerNameInput instance = FindObjectOfType<PlayerNameInput>();
        if (instance != null && instance.nameInputField != null)
        {
            instance.nameInputField.gameObject.SetActive(true);
            instance.nameInputField.text = string.Empty;
        }
    }

    public static void ClearPlayerName()
    {
        playerName = string.Empty;
        PlayerNameInput instance = FindObjectOfType<PlayerNameInput>();
        if (instance != null && instance.nameInputField != null)
        {
            instance.nameInputField.text = string.Empty;
        }
    }
}
