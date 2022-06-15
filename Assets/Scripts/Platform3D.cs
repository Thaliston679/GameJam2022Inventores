using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Platform3D : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private float vision3d;
    public float tilemapColor = 0;

    private void Start()
    {
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
    }

    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        

        vision3d = player.GetComponent<PlayerMove>().GetVision3dColor();

        switch (vision3d)
        {
            case 1:
                if (vision3d == tilemapColor)
                {
                    spriteRenderer.color = new Vector4(0.2f, 0.4f, 1, 1);//Azul Aparente
                    boxCollider2D.enabled = true;
                }
                else
                {
                    spriteRenderer.color = new Vector4(1, 0.2f, 0.2f, 0.2f);//Vermelho Apagado
                    boxCollider2D.enabled = false;
                }
                break;
            case 2:
                if (vision3d == tilemapColor)
                {
                    spriteRenderer.color = new Vector4(1, 0.2f, 0.2f, 1);//Vermelho Aparente
                    boxCollider2D.enabled = true;
                }
                else
                {
                    spriteRenderer.color = new Vector4(0.2f, 0.4f, 1, 0.2f);//Azul Apagado
                    boxCollider2D.enabled = false;
                }
                break;
            default:
                break;
        }
    }
}
