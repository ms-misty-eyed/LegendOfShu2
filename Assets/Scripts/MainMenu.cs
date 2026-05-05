using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject intro;
    public GameObject bg;
    public GameObject menu;
    public GameObject playButton;
    public GameObject escButton;

    private bool _waitingForSpace = false;

    public void Start()
    {
        intro.SetActive(false);
        bg.SetActive(false);
    }

    void Update()
    {
        if (_waitingForSpace)
    {
        Debug.Log("Waiting for space...");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    }

    public void PlayGame()
    {
        _waitingForSpace = true; // now Update() starts listening for Space
        Debug.Log("space = true");
        intro.SetActive(true);
        bg.SetActive(true);
        menu.SetActive(false);
        playButton.SetActive(false);
        escButton.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}