using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using StarterAssets;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject pauseBackground;
    public GameObject settingsMenuUI;
    private bool isPaused = false;

    void Update()
    {
        // Check if player presses Escape (or 'P' for Pause)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escape pressed");
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseBackground.SetActive(false);
        settingsMenuUI.SetActive(true);
        Time.timeScale = 1f; // Game moves again
        isPaused = false;

       // Access the inputs and clear them so the camera doesn't jump
        var inputs = FindObjectOfType<StarterAssetsInputs>();
        if (inputs != null)
        {
            inputs.look = Vector2.zero;
        }

        Cursor.lockState = CursorLockMode.Locked; // Hide mouse
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseBackground.SetActive(true);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 0f; // Everything freezes!
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; // Show mouse
        Cursor.visible = true;
    }



    public void OpenSettings()
    {
        // This hides the main pause buttons and shows the settings
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f; // IMPORTANT: Reset time before leaving or the menu will be frozen!
        SceneManager.LoadScene("MenuScene"); // Change to your actual menu scene name
    }

    public void ToggleMute()
    {
        // This flips between 0 (silent) and 1 (full volume)
        AudioListener.volume = (AudioListener.volume > 0) ? 0 : 1;
        Debug.Log("Volume is now: " + AudioListener.volume);
    }
}