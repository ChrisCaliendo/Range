using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false && Input.GetKeyDown(KeyCode.I))
        {
            Pause();
        }
        else
        {
            if (isPaused == true && Input.GetKeyDown("i"))
            {
                Resume();
            }
        }
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
