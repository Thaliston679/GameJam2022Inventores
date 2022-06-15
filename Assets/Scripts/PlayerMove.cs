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

    public GameObject panelMenuPause;
    public GameObject pauseButton;

    public GameObject cameraAtk;
    public GameObject flashAtk;

    public GameObject effect3D;

    private float timerFlash = 0;

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
        PlayerActions();
        TimerFlashAtk();
        Pause();
    }

    
    void PlayerMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(moveInput * moveSpeed, player.velocity.y);
    }

    void PlayerJump()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space)) && !isJumping && coyoteTimer > 0)
        {
            player.AddForce(new Vector2(player.velocity.x, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            anim.SetBool("jump",true);
        }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space)) && !isGrounded && doubleJump && coyoteTimer <= 0)
        {
            player.velocity = new Vector2(player.velocity.x,0);
            player.AddForce(new Vector2(player.velocity.x, jumpForce), ForceMode2D.Impulse);
            doubleJump = false;
            anim.SetBool("jump", true);
        }

        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Space))
        {
            PlayerJumpCut();
        }
    }

    void PlayerJumpCut()
    {
        if(isJumping && player.velocity.y > 0)
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y * jumpCut);
        }
        if(player.velocity.y < 0)
        {
            anim.SetBool("fall", true);
        }
    }

    void PlayerActions()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(vision3dColor == 1)
            {
                vision3dColor = 2;
                Vector3 effect3DPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                GameObject effect3dI = Instantiate(effect3D, effect3DPos, Quaternion.identity);
                SpriteRenderer spriteRenderer = effect3dI.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Vector4(1, 0.2f, 0.2f, 1);//Vermelho Aparente
                Destroy(effect3dI, 0.3f);
            }
            else
            {
                vision3dColor = 1;
                Vector3 effect3DPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                GameObject effect3dI = Instantiate(effect3D, effect3DPos, Quaternion.identity);
                SpriteRenderer spriteRenderer = effect3dI.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Vector4(0.2f, 0.4f, 1, 1);//Azul Aparente
                Destroy(effect3dI, 0.3f);
            }
        }

        if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Mouse0)) && lantern)
        {
            PlayerFlash();
        }
    }

    void PlayerFlash()
    {
        lantern = false;
        if (imagem.flipX)
        {
            Vector3 cameraAtkPos = new Vector3(transform.position.x - 0.14f, transform.position.y + 0.32f, transform.position.z + 2);
            Vector3 flashAtkPos = new Vector3(transform.position.x - 0.25f, transform.position.y + 0.26f, transform.position.z + 5);

            //GameObject atkCamera = Instantiate(cameraAtk, cameraAtkPos.transform);
            GameObject atkCamera = Instantiate(cameraAtk, cameraAtkPos, Quaternion.identity);
            SpriteRenderer spriteRenderer = atkCamera.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
            //atkCamera.transform.eulerAngles = new Vector3(0, 0, 180);

            GameObject atkFlash = Instantiate(flashAtk, flashAtkPos, Quaternion.identity);
            atkFlash.transform.eulerAngles = new Vector3(0, 0, 180);

            atkCamera.transform.parent = this.transform;
            atkFlash.transform.parent = this.transform;

            Destroy(atkFlash, 0.1f);
            Destroy(atkCamera, 0.15f);

        }
        else
        {
            Vector3 cameraAtkPos = new Vector3(transform.position.x + 0.14f, transform.position.y + 0.32f, transform.position.z + 2);
            Vector3 flashAtkPos = new Vector3(transform.position.x + 0.25f, transform.position.y + 0.26f, transform.position.z + 5);

            GameObject atkCamera = Instantiate(cameraAtk, cameraAtkPos, Quaternion.identity);
            SpriteRenderer spriteRenderer = atkCamera.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = false;
            //atkCamera.transform.eulerAngles = new Vector3(0, 0, 0);

            GameObject atkFlash = Instantiate(flashAtk, flashAtkPos, Quaternion.identity);
            atkFlash.transform.eulerAngles = new Vector3(0, 0, 0);

            atkCamera.transform.parent = this.transform;
            atkFlash.transform.parent = this.transform;

            Destroy(atkFlash, 0.1f);
            Destroy(atkCamera, 0.15f);
        }
    }

    void TimerFlashAtk()
    {
        timerFlash += Time.deltaTime;
        if (timerFlash > 0.5f)
        {
            lantern = true;
            timerFlash = 0;
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

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                panelMenuPause.SetActive(true);
                pauseButton.SetActive(false);

            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                panelMenuPause.SetActive(false);
                pauseButton.SetActive(true);
            }
        }
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
        isJumping = false;
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

    public bool GetFlipX()
    {
        return imagem.flipX;
    }

    //Para plataforma azul aparente     0.2f, 0.4f, 1, 1
    //Para plataforma azul escondida    0.2f, 0.4f, 1, 0.2f
    //Para plataforma vermelha aparente     1, 0.2f, 0.2f, 1
    //Para plataforma vermelha escondida    1, 0.2f, 0.2f, 0.2f
}
