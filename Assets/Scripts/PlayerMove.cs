using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D player;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isJumping  = false;

    private int playerHp = 5;

    private bool vision3D = false;
    private bool lantern = false;

    public int vision3dColor = 0; // 1 = Azul; 2 = Vermelho; 0 = Desativado



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerJump();
    }

    void PlayerMovement()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            player.velocity = new Vector2(moveSpeed, player.velocity.y);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            player.velocity = new Vector2(-moveSpeed, player.velocity.y);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            player.AddForce(new Vector2(player.velocity.x, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }


}
