using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> objectsToShow; // drag all objects you want shown here
    public List<GameObject> objectsToHide; // drag all objects you want hidden here

    private bool _waitingForSpace = false;

    public void Start()
    {
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
        if (_waitingForSpace)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void PlayGame()
    {
        _waitingForSpace = true;

        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}