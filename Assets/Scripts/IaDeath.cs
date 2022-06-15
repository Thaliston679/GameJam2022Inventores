using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaDeath : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool iaMorreu = GetComponent<IaEnemy>().GetEnemyDestroyed();
        if (iaMorreu)
        {
            RadioQuebrado();
        }
    }
    void RadioQuebrado()
    {
        anim.SetBool("morreu", true);
        BoxCollider2D iaCol = GetComponent<BoxCollider2D>();
        iaCol.enabled = false;
    }
}
