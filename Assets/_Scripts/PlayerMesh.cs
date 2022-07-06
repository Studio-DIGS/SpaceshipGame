using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour
{
    public Player player;
    public float smooth;

    void Update()
    {
        // Consider using Unity events instead of constantly changing the rotation every frame
        float orientation = player.orientation;
        float angle = Mathf.Clamp(orientation * 181, -181, 0);
        Quaternion targetRot = Quaternion.Euler(0,angle,0);
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smooth);
    }
}
