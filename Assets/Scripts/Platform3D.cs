using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform3D : MonoBehaviour
{
    public GameObject player;
    private float vision3d;

    private void Start()
    {
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        vision3d = player.GetComponent<PlayerMove>().GetVision3dColor();
    }
}
