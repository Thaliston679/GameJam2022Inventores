using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool movingRight = true;

    public float velPlat = 3f; //Velocidade da plataforma
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

        if (movingRight) //Move a plataforma para direita
        {
            inimigo.transform.position = new Vector2(inimigo.transform.position.x + velPlat * Time.deltaTime, inimigo.transform.position.y);
        }
        else //Move a plataforma para esquerda
        {
            inimigo.transform.position = new Vector2(inimigo.transform.position.x - velPlat * Time.deltaTime, inimigo.transform.position.y);
        }
    }
}
