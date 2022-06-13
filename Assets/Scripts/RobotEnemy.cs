using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEnemy : MonoBehaviour
{
    public Transform inimigo;
    public Transform moveRight;
    public Transform moveLeft;
    private bool movingRight;
    public float speed;

    void Start()
    {    
    }

    void Update()
    {
        if (inimigo.transform.position.x < moveRight.position.x)
        {
            movingRight = true;
        }
        if(inimigo.transform.position.x > moveLeft.position.x)
        {
            movingRight=false;
        }
        if (movingRight)
        {
            inimigo.transform.position = new Vector2(inimigo.transform.position.x + speed * Time.deltaTime, inimigo.transform.position.y);
        }
        else
        {
            inimigo.transform.position = new Vector2(inimigo.transform.position.x - speed * Time.deltaTime, inimigo.transform.position.y);
        }
    }
}
