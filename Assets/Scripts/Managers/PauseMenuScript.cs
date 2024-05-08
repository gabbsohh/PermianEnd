using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    void Stop()
    {
        pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            Debug.Log("Game Paused!");
        paused = true;
    }

    public void Play()
    {
        pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            Debug.Log("Game Resumed!");
        paused = false;
    }

    public void MainMenuButton()
    {   
        Time.timeScale = 1.0f;
        Debug.Log("Loading Title Screen!");
        SceneManager.LoadScene("Title");
    }

    public void SettingsButton()
    {
        Debug.Log("Going To Settings Menu!");
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackButton()
    {
        Debug.Log("Leaving Settings Menu!");
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
