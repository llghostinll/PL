using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinManager : MonoBehaviour
{
    // Call this method when the player wins the game
    public void LoadWinScreen()
    {
        // You can also add any necessary logic here (like playing a sound or an animation)
        // Load the "You Win" scene
        SceneManager.LoadScene("YouWinScene");
    }

    // Optional: Example method to check win condition
    public void CheckWinCondition(bool hasWon)
    {
        if (hasWon) // Replace with your actual win condition
        {
            LoadWinScreen();
        }
    }
}