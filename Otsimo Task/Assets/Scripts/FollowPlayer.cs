using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;
    void Update()
    {
        CameraPosition();
    }

    public void CameraPosition()
    {
        var cameraPos = player.position + offset;
        transform.position = cameraPos;
    }
}
