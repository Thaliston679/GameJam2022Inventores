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

    // Update is called once per frame
    void Update()
    {
        Tilemap spriteRenderer = GetComponent<Tilemap>();

        vision3d = player.GetComponent<PlayerMove>().GetVision3dColor();

        switch (vision3d)
        {
            case 1:
                if (vision3d == tilemapColor)
                {
                    spriteRenderer.color = new Vector4(0.2f, 0.4f, 1, 1);//Azul Aparente
                }
                else
                {
                    spriteRenderer.color = new Vector4(1, 0.2f, 0.2f, 0.2f);//Vermelho Apagado
                }
                break;
            case 2:
                if (vision3d == tilemapColor)
                {
                    spriteRenderer.color = new Vector4(1, 0.2f, 0.2f, 1);//Vermelho Aparente
                }
                else
                {
                    spriteRenderer.color = new Vector4(0.2f, 0.4f, 1, 0.2f);//Azul Apagado
                }
                break;
            default:
                break;
        }
    }
}
