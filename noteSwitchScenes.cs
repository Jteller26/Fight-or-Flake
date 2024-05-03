// This script switches scenes when the activator object collides with another object tagged as "Activator".

using UnityEngine;
using UnityEngine.SceneManagement;

public class noteSwitchScenes : MonoBehaviour
{
    public int sceneToLoad;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Activator"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
