// This script handles the activation of notes
// It allows for interaction with specific notes based on player input and triggers events
// such as adding score, playing sound effects, and activating particle effects.

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class Activator : MonoBehaviour
{
    bool active = false;
    GameObject gc;
    SpriteRenderer spriteRenderer;
    public Sprite correctSprite;
    Color originalColor;

    List<GameObject> activeNotes = new List<GameObject>();

    public bool createMode;
    public GameObject notePrefab;

    public InputActionReference inputActionNote1;
    public InputActionReference inputActionNote2;
    public InputActionReference inputActionNote3;
    public InputActionReference inputActionNote4;

    public gameComplete gameCompleteScript;
    public Mashing Mashing;

    public AudioSource keyPressedSound;

    public ParticleSystem particleEffect;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        if (inputActionNote1 != null)
        {
            inputActionNote1.action.Enable();
            inputActionNote1.action.performed += ctx => OnNote1Pressed();
        }

        if (inputActionNote2 != null)
        {
            inputActionNote2.action.Enable();
            inputActionNote2.action.performed += ctx => OnNote2Pressed();
        }

        if (inputActionNote3 != null)
        {
            inputActionNote3.action.Enable();
            inputActionNote3.action.performed += ctx => OnNote3Pressed();
        }

        if (inputActionNote4 != null)
        {
            inputActionNote4.action.Enable();
            inputActionNote4.action.performed += ctx => OnNote4Pressed();
        }
    }
    private void Start()
    {
        if (gameCompleteScript == null)
        {
            gameCompleteScript = Object.FindFirstObjectByType<gameComplete>();
        }
    }
    private void OnNote1Pressed()
    {
        HandleNoteInputWithTag("Note1");
    }
    private void OnNote2Pressed()
    {
        HandleNoteInputWithTag("Note2");
    }
    private void OnNote3Pressed()
    {
        HandleNoteInputWithTag("Note3");
    }
    private void OnNote4Pressed()
    {
        HandleNoteInputWithTag("Note4");
    }
    private void HandleNoteInputWithTag(string tag)
    {
        bool clickedInTime = false;
        foreach (GameObject note in new List<GameObject>(activeNotes))
        {
            if (active && note && note.CompareTag(tag))
            {
                if (GetComponent<Collider2D>().IsTouching(note.GetComponent<Collider2D>()))
                {
                    Destroy(note);
                    StartCoroutine(ChangeActivatorSprite());
                    AddScore();
                    active = false;
                    clickedInTime = true;
                    if (keyPressedSound != null)
                        keyPressedSound.Play();
                    if (particleEffect != null)
                    {
                        particleEffect.Play();
                    }
                }
            }
        }
        if (createMode)
        {
            GameObject newNote = Instantiate(notePrefab, transform.position, Quaternion.identity);
            newNote.tag = tag;
        }
        if (!clickedInTime)
        {
            if (gameCompleteScript != null)
            {
                gameCompleteScript.ResetStreak();
            }
        }
        activeNotes.Clear();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
        string tag = collision.gameObject.tag;
        if (tag == "Note1" || tag == "Note2" || tag == "Note3" || tag == "Note4")
        {
            activeNotes.Add(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Note1" || tag == "Note2" || tag == "Note3" || tag == "Note4")
        {
            gameCompleteScript?.ResetStreak();
            Destroy(collision.gameObject);
            activeNotes.Clear();
        }
    }
    public void AddScore()
    {
        int scoreToAdd = 10 * (gameCompleteScript?.GetMultiplier() ?? 1);
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + scoreToAdd);
        gameCompleteScript?.AddStreak();
    }
    IEnumerator ChangeActivatorSprite()
    {
        Sprite originalSprite = spriteRenderer.sprite;
        spriteRenderer.color = new Color(0, 1, 0);
        spriteRenderer.sprite = correctSprite;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.sprite = originalSprite;
        spriteRenderer.color = originalColor;
    }
    private void OnDisable()
    {
        if (inputActionNote1 != null)
        {
            inputActionNote1.action.Disable();
        }

        if (inputActionNote2 != null)
        {
            inputActionNote2.action.Disable();
        }

        if (inputActionNote3 != null)
        {
            inputActionNote3.action.Disable();
        }

        if (inputActionNote4 != null)
        {
            inputActionNote4.action.Disable();
        }
    }
}
