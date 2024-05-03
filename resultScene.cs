// This script manages the result scene, displaying the player's score, high score, star rating, and updating the total stars collected across all levels.

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    public int levelNumber;
    public Image[] starImages;
    public TextMeshProUGUI highScoreText;

    public int oneStarThreshold;
    public int twoStarThreshold;
    public int threeStarThreshold;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score");
        int highScore = PlayerPrefs.GetInt("Level" + levelNumber + "HighScore", 0);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("Level" + levelNumber + "HighScore", highScore);
        }
        highScoreText.text = highScore.ToString();

        int starRating = CalculateStarRating(score);
        DisplayStars(starRating);

        int totalStars = PlayerPrefs.GetInt("Level" + levelNumber + "Stars", 0);
        Debug.Log("Total Stars for Level " + levelNumber + ": " + totalStars);
        UpdateTotalStarsCollected();
    }

    int CalculateStarRating(int score)
    {
        if (score <= oneStarThreshold)
            return 0;
        else if (score <= twoStarThreshold)
            return 1;
        else if (score <= threeStarThreshold)
            return 2;
        else
            return 3;
    }

    void DisplayStars(int starRating)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < starRating)
            {
                starImages[i].enabled = true;
            }
            else
            {
                starImages[i].enabled = false;
            }
        }
        int currentStars = PlayerPrefs.GetInt("Level" + levelNumber + "Stars", 0);
        if (starRating > currentStars)
        {
            PlayerPrefs.SetInt("Level" + levelNumber + "Stars", starRating);
        }
        UpdateTotalStarsCollected();
    }

    void UpdateTotalStarsCollected()
    {
        int totalStarsCollected = 0;
        for (int i = 1; i <= 4; i++) // 4 (How many Levels)
        {
            totalStarsCollected += PlayerPrefs.GetInt("Level" + i + "Stars", 0);
        }
        PlayerPrefs.SetInt("TotalStarsCollected", totalStarsCollected);
        Debug.Log("Total Stars collected across all levels: " + totalStarsCollected);
    }
}