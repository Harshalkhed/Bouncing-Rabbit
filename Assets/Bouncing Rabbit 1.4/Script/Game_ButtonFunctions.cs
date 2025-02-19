using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_ButtonFunctions : MonoBehaviour
{
    // Opens a URL for rating the app
    public void Rate()
    {
        // Replace with your app's Play Store URL
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.example");
    }

    // Pauses the game and optionally shows a paused ad
    public void Pause()
    {
        GameController.instance.showpausedAd(); // Show a paused ad
        GameController.instance.Pause();       // Pause the game
    }

    // Resumes the game
    public void Resume()
    {
        GameController.instance.Resume();      // Resume the game
    }

    // Navigates to the Home screen (StartMenu scene)
    public void Home()
    {
        // Load the StartMenu scene
        SceneManager.LoadScene("StartMenu");

        // Ensure the game is not paused
        Time.timeScale = 1;
    }

    // Restarts the current scene
    public void Restart()
    {
        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Ensure the game is not paused
        Time.timeScale = 1;
    }
}
