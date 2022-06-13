using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItenMovement : MonoBehaviour
{
    public bool moveUp = true;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    IEnumerator Revert()
    {
        yield return new WaitForSeconds(1f);
        moveUp = false;
        yield return new WaitForSeconds(1f);
        moveUp = true;
    }
    void Update()
    {
        if (moveUp)
        {
            StartCoroutine("Revert");
            if (transform.position.y > startPosition.y + 1f)
            {
                transform.position = new Vector2(transform.position.x, startPosition.y + 0.3f);
            }
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f * Time.deltaTime);
        }
    }

    public Vector3 GetStartPosistion()
    {
        return startPosition;
    }
}
