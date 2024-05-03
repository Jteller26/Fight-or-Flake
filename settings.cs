// This script manages the settings menu, allowing the player to adjust the volume and resume gameplay.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    public GameObject settingsMenuUI;
    public Slider volumeSlider;

    private float defaultVolume = 0.5f;
    private string volumeKey = "Volume";

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(volumeKey, defaultVolume);
        SetVolume();
    }
    public void OnButtonClick()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        settingsMenuUI.SetActive(true);
    }
    public void Resume()
    {
        settingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
    public void LoadScene(int sceneNumber)
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(sceneNumber);
    }
    public void SetVolume()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(volumeKey, volume);
    }
}
