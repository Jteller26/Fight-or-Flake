// This script manages game completion functionality such as streak counting, multiplier calculation,
// updating GUI elements, triggering animations, and handling score additions.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameComplete : MonoBehaviour
{
    int multiplier = 1;
    int streak = 0;

    public Mashing mashingScript;
    public AudioSource wrongNoteSound;

    public Animator animator;

    public Image snowScreen;
    public Image multStar;

    bool canTriggerAnimation = true;

    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Multiplier", 1);
        PlayerPrefs.SetInt("Streak", 0);
    }
    public void AddStreak()
    {
        streak++;
        if (streak % 10 == 0 && streak != 10 && streak < 51)
        {
            if (animator != null && canTriggerAnimation)
            {
                if (!IsAnimationPlaying("Snowman"))
                {
                    animator.SetTrigger("Glove");
                    StartCoroutine(star());
                }
            }
        }

        if (streak < 20)
            multiplier = 1;
        else if (streak < 30)
            multiplier = 2;
        else if (streak < 40)
            multiplier = 3;
        else if (streak < 50)
            multiplier = 4;
        else
            multiplier = 5;
        UpdateGUI();
    }
    public void ResetStreak()
    {
        multiplier = 1;
        streak = 0;
        UpdateGUI();
        if (wrongNoteSound != null)
            wrongNoteSound.Play();
        if (animator != null && canTriggerAnimation)
        {
            StartCoroutine(TriggerAnimationDelay());
            StartCoroutine(Screen());
        }
    }
    IEnumerator TriggerAnimationDelay()
    {
        canTriggerAnimation = false;
        if (!IsAnimationPlaying("Glove"))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Snowman"))
            {
                animator.SetTrigger("Snowman");
            }
        }
        yield return new WaitForSeconds(1.5f);
        canTriggerAnimation = true;
    }

    IEnumerator Screen()
    {
        yield return new WaitForSeconds(1f);
        snowScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        snowScreen.gameObject.SetActive(false);
    }
    IEnumerator star()
    {
        yield return new WaitForSeconds(.7f);
        multStar.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        multStar.gameObject.SetActive(false);
    }
    private bool IsAnimationPlaying(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
    private void UpdateGUI()
    {
        PlayerPrefs.SetInt("Streak", streak);
        PlayerPrefs.SetInt("Multiplier", multiplier);
    }
    public int GetMultiplier()
    {
        return multiplier;
    }
    public void AddScoreFromMashing(int scoreToAdd)
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + scoreToAdd);
    }
}
