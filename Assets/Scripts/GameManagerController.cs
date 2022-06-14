using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public static void PauseGame()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }
}
