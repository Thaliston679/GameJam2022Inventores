using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool movingRight = true;

    public float velEnemy = 3f;
    public Transform inimigo;
    public Transform moveRight;
    public Transform moveLeft;

    void Update()
    {
        if (inimigo.transform.position.x < moveRight.position.x)
        {
            movingRight = true;
        }
        if (inimigo.transform.position.x > moveLeft.position.x)
        {
            movingRight = false;
        }

        if (movingRight)
        {
            inimigo.transform.position = new Vector2(inimigo.transform.position.x + velEnemy * Time.deltaTime, inimigo.transform.position.y);
            inimigo.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            inimigo.transform.position = new Vector2(inimigo.transform.position.x - velEnemy * Time.deltaTime, inimigo.transform.position.y);
            inimigo.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
