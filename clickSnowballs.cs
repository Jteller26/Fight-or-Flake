// This script handles the spawning and interaction of snowball objects on a canvas.
// It allows players to click on snowballs, earning score points, and handles mechanics
// such as countdown before starting, timing duration, and disabling clicking after a certain duration.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class clickSnowballs : MonoBehaviour
{
    public GameObject canvasSnowball;
    private Vector2 spawnAreaSize;
    public float spawnInterval = 2f;
    public float imageLifetime = 3f;

    private List<Vector2> occupiedPositions = new List<Vector2>();

    public Activator activatorScript;
    public gameComplete gameCompleteScript;

    public float startDelay = 10f;
    public float countdownDuration = 3f;
    public float duration = 30f;
    public bool clickingEnabled = true;

    public TextMeshProUGUI countdownText;

    public AudioSource keyPressedSound;

    void Start()
    {
        spawnAreaSize = new Vector2(10f, 6f);
        StartCoroutine(StartMechanicWithDelay());
    }
    IEnumerator StartMechanicWithDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(StartCountdown());
    }
    IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = "Snowballs\n" + i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "";
        StartMechanic();
    }
    void StartMechanic()
    {
        clickingEnabled = true;
        StartCoroutine(SpawnCanvasCoroutine());
        StartCoroutine(EndMechanicAfterDuration());
    }
    IEnumerator EndMechanicAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        clickingEnabled = false;
    }
    IEnumerator SpawnCanvasCoroutine()
    {
        while (clickingEnabled)
        {
            SpawnCanvas();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void SpawnCanvas()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject spawnedCanvas = Instantiate(canvasSnowball, spawnPosition, Quaternion.identity);
        Button button = spawnedCanvas.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => {
            Destroy(spawnedCanvas);
            activatorScript.AddScore();
            if (keyPressedSound != null)
                keyPressedSound.Play();
        });
        StartCoroutine(DestroyIfNotClicked(spawnedCanvas));
    }
    IEnumerator DestroyIfNotClicked(GameObject canvas)
    {
        yield return new WaitForSeconds(imageLifetime);
        if (canvas != null)
        {
            Destroy(canvas);
            gameCompleteScript.ResetStreak();
        }
    }
    Vector2 GetRandomSpawnPosition()
    {
        float x, y;
        Vector2 spawnPosition;
        do
        {
            x = Random.Range(-2f, 7.5f);
            y = Random.Range(-.5f, 3f);
            spawnPosition = new Vector2(x, y);
        }
        while (IsTooCloseToOccupiedPosition(spawnPosition));
        return spawnPosition;
    }
    bool IsTooCloseToOccupiedPosition(Vector2 position)
    {
        foreach (Vector2 occupiedPosition in occupiedPositions)
        {
            if (Vector2.Distance(position, occupiedPosition) < 4f)
            {
                return true;
            }
        }
        return false;
    }
}
