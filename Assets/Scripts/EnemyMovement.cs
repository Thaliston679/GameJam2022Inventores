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

    private bool quebrado = false;

    void Update()
    {
        if(quebrado == false)
        { 
            MoveEnemy();
        }
    }

    void MoveEnemy()
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
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("flashAtk"))
        {
            quebrado = true;
        }
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("flashAtk"))
        {
            quebrado = true;
        }
    }

    public bool GetEnemyDestroyed()
    {
        return quebrado;
    }
}
