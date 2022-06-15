using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDeath : MonoBehaviour
{
    public Animator anim;

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
        BoxCollider2D robotCol = GetComponent<BoxCollider2D>();
        robotCol.enabled = false;
    }
}
