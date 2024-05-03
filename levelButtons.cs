// This script handles the behavior of level buttons, including displaying level details,
// showing high scores and star ratings, and managing canvas visibility on hover and click for level buttons.

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int levelNumber;
    public GameObject levelDetailsCanvas;
    public Image[] starImages;
    public TextMeshProUGUI highScoreText;
    public GameObject panelGameObject;

    private bool isCanvasActive = false;
    private bool keepCanvasActiveOnClick = false;
    private bool isHovering = false;

    private void Start()
    {
        levelDetailsCanvas.SetActive(false);
        DisplayLevelInfo();
    }
    public void OnButtonClick()
    {
        levelDetailsCanvas.SetActive(true);
        keepCanvasActiveOnClick = true;
        panelGameObject.SetActive(true);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        if (!isCanvasActive && !keepCanvasActiveOnClick)
        {
            levelDetailsCanvas.SetActive(true);
            isCanvasActive = true;
        }
        panelGameObject.SetActive(false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        if (!keepCanvasActiveOnClick)
        {
            StartCoroutine(HideCanvasDelayed());
        }
    }
    public void OnExitButtonClick()
    {
        levelDetailsCanvas.SetActive(false);
        isCanvasActive = false;
        keepCanvasActiveOnClick = false;
        panelGameObject.SetActive(false);
    }
    IEnumerator HideCanvasDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        levelDetailsCanvas.SetActive(false);
        isCanvasActive = false;
        if (!isHovering)
        {
            panelGameObject.SetActive(true);
        }
    }
    void DisplayLevelInfo()
    {
        int totalStars = PlayerPrefs.GetInt("Level" + levelNumber + "Stars", 0);
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].enabled = i < totalStars;
        }
        int highScore = PlayerPrefs.GetInt("Level" + levelNumber + "HighScore", 0);
        highScoreText.text = highScore.ToString();
    }
}
