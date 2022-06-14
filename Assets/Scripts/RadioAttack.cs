using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAttack : MonoBehaviour
{

    public float velPlat = 3f; //Velocidade da plataforma
    public GameObject tiro1;
    public GameObject tiro2;
    public Transform moveRight;
    public Transform moveLeft;

    void Update()
    {
        tiro2.transform.position = new Vector2(tiro2.transform.position.x + velPlat * Time.deltaTime, tiro2.transform.position.y);
        Destroy(tiro2.gameObject, 1.5F);
        
       
        tiro1.transform.position = new Vector2(tiro1.transform.position.x - velPlat * Time.deltaTime, tiro1.transform.position.y);
        Destroy(tiro1.gameObject, 1.5F);
    }

   
}
