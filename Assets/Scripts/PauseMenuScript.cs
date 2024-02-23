using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenuCanvas;

    // attempting to commit on different computer

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
        pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0.0f;
        paused = true;
    }

    public void Play()
    {
        pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1.0f;
        paused = false;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Title");
    }
}
