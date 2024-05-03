// This script handles resetting player preferences, showing/hiding a prompt for starting a new game.

using UnityEngine;
using UnityEngine.SceneManagement;

public class resetGame : MonoBehaviour
{
    public GameObject newGameCanvas;

    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All player preferences have been reset.");
    }

    public void ShowNewGamePrompt()
    {
        newGameCanvas.SetActive(true);
    }
    public void ExitNewGamePrompt()
    {
        newGameCanvas.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exited game");
    }
}
