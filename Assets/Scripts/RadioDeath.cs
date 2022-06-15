using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioDeath : MonoBehaviour
{
    private Animator anim;
    public GameObject radioAtk;

    private void Update()
    {
        bool quebrou = GetComponent<EnemyMovement>().GetEnemyDestroyed();
        if (quebrou)
        {
            RadioQuebrado();
        }
    }
    void RadioQuebrado()
    {
        anim.SetBool("quebrou", true);
        BoxCollider2D radioCol = GetComponent<BoxCollider2D>();
        radioCol.enabled = false;
        Destroy(radioAtk);
    }
}
