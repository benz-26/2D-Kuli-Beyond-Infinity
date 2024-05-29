using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;

    private Vector3 startPos;
    private Vector3 followPos;
    private float yPos;

    void Awake()
    {

        followPos = new Vector3(transform.position.x, 0, transform.position.z);
        startPos = transform.position;
    }

    void Update()
    {
            Follow();

    }

    void Follow()
    {

        yPos = Mathf.Max(yPos, player.position.y);

        transform.position = followPos + (Vector3.up * yPos);

    }
}
