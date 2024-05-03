// This script manages the pause menu functionality, allowing the player to pause the game, resume gameplay, and load different scenes.

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public InputActionReference inputAction;

    void Start()
    {
        inputAction.action.Enable();
    }
    void Update()
    {
        if (inputAction.action.triggered)
        {
            OnButtonClick();
        }
    }
    public void OnButtonClick()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
    public void LoadScene(int sceneNumber)
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(sceneNumber);
    }
}
