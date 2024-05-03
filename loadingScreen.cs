// This script handles the loading screen functionality, including displaying the loading progress,
// hiding the main menu, and allowing the player to start the level when loading is complete.

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class loadingScreen : MonoBehaviour
{
    public GameObject loadScreen;
    public GameObject mainMenu;
    public Button startButton;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;

    private bool loadingComplete = false;
    private AsyncOperation loadOperation;

    public void LoadLevelClick(int level)
    {
        mainMenu.SetActive(false);
        loadScreen.SetActive(true);
        startButton.gameObject.SetActive(false);

        StartCoroutine(LoadLevelAsync(level));
    }
    IEnumerator LoadLevelAsync(int level)
    {
        loadOperation = SceneManager.LoadSceneAsync(level);
        loadOperation.allowSceneActivation = false;

        while (!loadingComplete)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;

            if (loadOperation.progress >= 0.9f)
            {
                startButton.gameObject.SetActive(true);
                loadingText.gameObject.SetActive(false);
                loadingSlider.gameObject.SetActive(false);
                loadingComplete = true;
            }

            yield return null;
        }
    }
    public void StartLevel()
    {
        loadOperation.allowSceneActivation = true;
    }
}
