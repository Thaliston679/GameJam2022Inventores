using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalCredits : MonoBehaviour
{

    public GameObject credits;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            credits.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
