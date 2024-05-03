// This script handles level selection and loading different scenes, including loading the credits scene.

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    private const string creditsKey = "CreditsShown";
    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }
    public void Continue(int sceneNumber1)
    {
        SceneManager.LoadSceneAsync(sceneNumber1);
    }
    public void LoadCredits(int creditsNumber)
    {
        bool creditsShown = PlayerPrefs.GetInt(creditsKey, 0) == 1;

        if (!creditsShown)
        {
            SceneManager.LoadSceneAsync(11);
            PlayerPrefs.SetInt(creditsKey, 1);
        }
        else
        {
            SceneManager.LoadSceneAsync(creditsNumber);
        }
    }
}
