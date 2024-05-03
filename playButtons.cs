// This script manages the play button's availability based on the total stars collected by the user to go to next level.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playButton : MonoBehaviour
{
    public int totalStarsThreshold;
    public TextMeshProUGUI requirementNumber;
    public Image requirementImage;


    private Button playButton_;

    private void Start()
    {
        playButton_ = GetComponent<Button>();
        UpdatePlayButtonAvailability();
    }

    private void UpdatePlayButtonAvailability()
    {
        int totalStarsCollected = PlayerPrefs.GetInt("TotalStarsCollected", 0);
        int starsNeeded = totalStarsThreshold - totalStarsCollected;

        if (starsNeeded <= 0)
        {
            requirementNumber.enabled = false;
            requirementImage.enabled = false;
            playButton_.interactable = true;
        }
        else
        {
            requirementNumber.text = starsNeeded.ToString();
            requirementNumber.enabled = true;
            requirementImage.enabled = true;
            playButton_.interactable = false;
        }
    }
}
