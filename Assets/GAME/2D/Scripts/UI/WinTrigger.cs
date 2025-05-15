using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    // This method is called when another collider enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered is the player
        if (other.CompareTag("Player"))
        {
            LoadWinScene();
        }
    }

    // Method to load the "YouWin" scene
    private void LoadWinScene()
    {
        // Load the "YouWin" scene
        SceneManager.LoadScene("YouWin");
    }
}