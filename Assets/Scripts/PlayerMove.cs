using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D player;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    private float jumpCut = 0.25f; //(0 - 0.5f)
    public SpriteRenderer imagem;
    private Animator anim;

    //meu index atual
    

    private bool doubleJump = false;

    private bool isGrounded = false;

    private float direction = 1;

    private float coyoteTime = 0.1f;
    private float coyoteTimer;

    private int playerHp = 5;

    private bool vision3D = false;
    private bool lantern = false;

    public int vision3dColor = 0; // 1 = Azul; 2 = Vermelho; 0 = Desativado

    private float moveInput;

    void Start()
    { 
        anim = GetComponent<Animator>();
        imagem = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerJump();
        PlayerMirror();
        PlayerCoyoteTimer();
    }

    
    void PlayerMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(moveInput * moveSpeed, player.velocity.y);

        /*
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
        */
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping && coyoteTimer > 0)
        {
            player.AddForce(new Vector2(player.velocity.x, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            anim.SetBool("jump",true);
        }

        if (Input.GetButtonDown("Jump") && !isGrounded && doubleJump && isJumping && coyoteTimer <= 0)
        {
            player.velocity = new Vector2(player.velocity.x,0);
            player.AddForce(new Vector2(player.velocity.x, jumpForce), ForceMode2D.Impulse);
            doubleJump = false;
            anim.SetBool("jump", true);
        }

        if (Input.GetButtonUp("Jump"))
        {
            PlayerJumpCut();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            vision3dColor = 1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            vision3dColor = 2;
        }
    }

    void PlayerJumpCut()
    {
        if(isJumping && player.velocity.y > 0)
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y * jumpCut);
            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                player.AddForce(new Vector2(player.velocity.x, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                anim.SetBool("jump", true);
            }
        }
        if(isJumping && player.velocity.y < 0)
        {
            anim.SetBool("fall", true);
        }
    }

    void PlayerMirror()
    {
        direction = Input.GetAxisRaw("Horizontal");
        if(direction > 0)
        {
            imagem.flipX = false;
            anim.SetBool("run", true);          
        }
        if (direction < 0)
        {
            imagem.flipX =true;
            anim.SetBool("run",true);          
        }
        if(direction == 0)
        {
            anim.SetBool("run",false);          
        }

    }

    void PlayerCoyoteTimer()
    {
        coyoteTimer -= Time.deltaTime;
        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
    }

    void Morrer()
    {
        Reiniciar();
    }

    void Reiniciar()
    {
        int cenaAtual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(cenaAtual);
    }

    void Fase2()
    {
        SceneManager.LoadScene(2);
    }

    void Fase3()
    {
        SceneManager.LoadScene(3);
    }

    void Fase4()
    {
        SceneManager.LoadScene(4);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Tocou o chão");
            isGrounded = true;
            isJumping = false;
            doubleJump = true;
        }

        if (collision.gameObject.CompareTag("PassarFase2"))
        {
            Fase2();
        }

        if (collision.gameObject.CompareTag("PassarFase3"))
        {
            Fase3();
        }

        if (collision.gameObject.CompareTag("PassarFase4"))
        {
            Fase4();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHp = 0;
        }

        if (playerHp <= 0)
        {
            Morrer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
        }
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Saiu do chão");
            isGrounded = false;
        }
    }

    public int GetVision3dColor()
    {
        return vision3dColor;
    }

    //Para plataforma azul aparente     0.2f, 0.4f, 1, 1
    //Para plataforma azul escondida    0.2f, 0.4f, 1, 0.2f
    //Para plataforma vermelha aparente     1, 0.2f, 0.2f, 1
    //Para plataforma vermelha escondida    1, 0.2f, 0.2f, 0.2f
}
