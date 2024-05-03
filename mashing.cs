// This script manages the mashing skill, where the player has a limited time to mash a button repeatedly
// to increase their score.

using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class Mashing : MonoBehaviour
{
    public InputActionReference inputAction;
    public gameComplete gameCompleteScript;
    public Image mashImage;

    public float duration = 30f;
    public float startDelay = 10f;
    public TextMeshProUGUI text;

    public bool mashingEnabled = true;

    float timer;
    int scoreMash;
    bool started;
    bool countdownStarted;

    private void Awake()
    {
        inputAction.action.Enable();
    }

    void Start()
    {
        mashImage.enabled = false;
        timer = duration;
        scoreMash = 0;
        PlayerPrefs.SetInt("MashingScore", 0);
        if (mashingEnabled)
            Invoke("StartCountdown", startDelay);
    }

    void StartCountdown()
    {
        countdownStarted = true;
        timer = 3;
    }

    void Update()
    {
        if (!mashingEnabled)
            return;

        if (countdownStarted)
        {
            timer -= Time.deltaTime;
            if (timer > 0)
            {
                text.text = "Mash\n" + Mathf.Ceil(timer).ToString();
            }
            else
            {
                mashImage.enabled = true;
                started = true;
                countdownStarted = false;
                timer = duration;
            }
        }
        else if (started)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;

                if (inputAction.action.triggered)
                {
                    scoreMash++;
                    PlayerPrefs.SetInt("MashingScore", scoreMash);
                }

                text.text = "Mash!";
                mashImage.enabled = true;
            }
            else
            {
                // Mashing completed, add score to the game
                scoreMash *= 10; // Multiply by 10 before adding to the game score
                gameCompleteScript.AddScoreFromMashing(scoreMash); // Add score to the game
                mashImage.enabled = false;
                started = false;
                StartCoroutine(DisplayMultiplierAndAddStreakForDuration());
            }
        }
    }

    IEnumerator DisplayMultiplierAndAddStreakForDuration()
    {
        text.text = "+" + scoreMash + "\nScore";
        gameCompleteScript.AddStreak();
        yield return new WaitForSeconds(3);
        text.text = "";
        scoreMash = 0;
        mashImage.enabled = false;
    }
}
