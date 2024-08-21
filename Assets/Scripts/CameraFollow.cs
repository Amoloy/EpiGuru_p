using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public Vector3 Offset;
    public float MinX = -2f;
    public float MaxX = 2f;

    void LateUpdate()
    {
        if (Player == null)
        {
            return;
        }
        
        Vector3 desiredPosition = Player.position + Offset;
        
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, MinX, MaxX);
        
        transform.position = desiredPosition;
    }
}
