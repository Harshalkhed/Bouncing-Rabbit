using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu_ButtonFunctions : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Handle back button input on mobile or Escape key on desktop
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame(); // Quit the application
        }
    }

    // Open the app's Google Play Store URL for rating
    public void Rate()
    {
        //Application.OpenURL("https://play.google.com/store/apps/details?id=com.example"); // Replace with your app's actual Play Store URL
    }

    // Load the main game scene
    public void Play()
    {
        SceneManager.LoadScene("Game"); // Replace "Game" with the actual name of your game scene
    }

    // Mute or unmute game sounds
    public void Mute()
    {
        SoundManager.instance.Mute(); // Ensure the SoundManager is correctly implemented
    }

    // Quit the application
    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // Debug log for the Unity Editor (since Application.Quit does nothing in the Editor)
    }
}
