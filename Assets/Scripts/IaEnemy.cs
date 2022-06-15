using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaEnemy : MonoBehaviour
{

    private bool movingRight = true;

    public float velEnemy = 3f;
    public Transform inimigo;
    public Transform moveRight;
    public Transform moveLeft;

    private bool quebrado = false;

    public GameObject laser;
    private float tempoTiro = 0;
    private bool pode_atirar;

    private float timerLaser = 0;

    void Update()
    {
        TimerLaserAtk();
        Ataque();
        if (quebrado == false)
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

    void Ataque()
    {
        if (movingRight)
        {
            Vector3 pontoDeDisparo = new Vector3(transform.position.x + 0.14f, transform.position.y, transform.position.z);
            GameObject laserDisparado = Instantiate(laser, pontoDeDisparo, Quaternion.identity);
            laserDisparado.GetComponent<Projectile>().DirecaoBala(0.08f);
            laser.transform.localScale = new Vector3(1, 1, 1);  
            
            Destroy(laser, 1.5f);
        }    
        else
        {
            Vector3 pontoDeDisparo = new Vector3(transform.position.x + 0.14f, transform.position.y, transform.position.z);
            GameObject laserDisparado = Instantiate(laser, pontoDeDisparo, Quaternion.identity);
            laserDisparado.GetComponent<Projectile>().DirecaoBala(0.08f);
            laser.transform.localScale = new Vector3(-1, 1, 1);

            Destroy(laserDisparado, 1.5f);
        }
    }

    void TimerLaserAtk()
    {
        timerLaser += Time.deltaTime;
        if (timerLaser > 0.5f)
        {
            timerLaser = 0;
        }
    }
}
