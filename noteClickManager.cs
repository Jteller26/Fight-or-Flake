// This script manages the note clicking mechanic, where the player must click on notes displayed on the screen.
// Score and streak will be incresed from clicking on snowballs

using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class NoteClickManager : MonoBehaviour
{
    public Activator activatorScript;
    public gameComplete gameCompleteScript;
    public Sprite correctNoteSprite;
    public Sprite wrongNoteSprite;
    public TextMeshProUGUI countdownText;

    public InputActionReference inputAction;

    private SpriteRenderer currentSpriteRenderer;
    private bool clicked;
    private bool started;

    public float startDelay;
    public float duration;
    public float countdownDuration;
    public float spawnInterval;
    public float lifetime;

    public AudioSource keyPressedSound;
    public AudioSource wrongNoteSound;

    private IEnumerator mechanicCoroutine;

    void Start()
    {
        if (inputAction != null)
        {
            inputAction.action.Enable();
            inputAction.action.started += OnInput;
        }

        currentSpriteRenderer = GetComponent<SpriteRenderer>();
        currentSpriteRenderer.enabled = false;

        StartCoroutine(StartCountdown());
    }

    void OnDestroy()
    {
        if (inputAction != null)
        {
            inputAction.action.Disable();
            inputAction.action.started -= OnInput;
        }
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(startDelay - countdownDuration);

        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = "Good VS. Bad\n" + i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "";

        StartMechanic();
        yield return new WaitForSeconds(duration);
        EndMechanic();
    }

    void StartMechanic()
    {
        started = true;
        mechanicCoroutine = MechanicCoroutine();
        StartCoroutine(mechanicCoroutine);
    }

    void EndMechanic()
    {
        started = false;
        if (mechanicCoroutine != null)
            StopCoroutine(mechanicCoroutine);
    }

    IEnumerator MechanicCoroutine()
    {
        while (started)
        {
            SpawnClickImages();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnClickImages()
    {
        currentSpriteRenderer.sprite = Random.value < 0.6f ? correctNoteSprite : wrongNoteSprite;
        currentSpriteRenderer.enabled = true;
        clicked = false;
        StartCoroutine(HideSpriteAfterLifetime());
    }

    IEnumerator HideSpriteAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);

        if (!clicked)
        {
            if (currentSpriteRenderer.sprite == wrongNoteSprite)
            {
                activatorScript.AddScore();
                if (keyPressedSound != null)
                    keyPressedSound.Play();
            }
            else if (currentSpriteRenderer.sprite == correctNoteSprite)
            {
                gameCompleteScript.ResetStreak();
                if (wrongNoteSound != null)
                    wrongNoteSound.Play();

            }
        }

        currentSpriteRenderer.enabled = false;
    }

    void OnInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentSpriteRenderer.enabled)
            {
                clicked = true;

                if (currentSpriteRenderer.sprite == correctNoteSprite)
                {
                    activatorScript.AddScore();
                    if (keyPressedSound != null)
                        keyPressedSound.Play();
                }
                else if (currentSpriteRenderer.sprite == wrongNoteSprite)
                {
                    gameCompleteScript.ResetStreak();
                    if (wrongNoteSound != null)
                        wrongNoteSound.Play();

                }

                currentSpriteRenderer.enabled = false;
            }
        }
    }
}
