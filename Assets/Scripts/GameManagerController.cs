using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public GameObject panelMenuPause;
    public GameObject pauseButton;

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            panelMenuPause.SetActive(true);
            pauseButton.SetActive(false);

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            panelMenuPause.SetActive(false);
            pauseButton.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
