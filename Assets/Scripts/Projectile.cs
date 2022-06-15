using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float velocidadeLaser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoverBala();
    }

    void MoverBala()
    {
        transform.position = new Vector3(transform.position.x + velocidadeLaser, transform.position.y, transform.position.z);
    }

    public void DirecaoBala(float direcao)
    {
        velocidadeLaser = direcao;
    }
}
