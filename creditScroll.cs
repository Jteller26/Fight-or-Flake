// This script controls the scrolling of credits text and provides functionality to skip the credits.

using UnityEngine;
using UnityEngine.SceneManagement;

public class creditScroll : MonoBehaviour
{
    public float scrollSpeed = 20f;

    private RectTransform creditsTextRectTransform;

    public int yValueSceneChange;

    private void Start()
    {
        creditsTextRectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        creditsTextRectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        if (creditsTextRectTransform.anchoredPosition.y >= yValueSceneChange)
        {
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    public void OnSkipClicked()
    {
        SceneManager.LoadScene(0);
    }
}