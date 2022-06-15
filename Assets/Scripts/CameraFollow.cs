using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float smoothSpeed;
    public float distX;
    public Vector3 offset;
    bool directionFlip = false;

    void FixedUpdate()
    {
        directionFlip = target.GetComponent<PlayerMove>().GetFlipX();
        offset.x = directionFlip ? -distX : distX;
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
