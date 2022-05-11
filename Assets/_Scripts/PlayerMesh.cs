using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour
{
    public Player player;
    public float smooth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: fix it so the player is always facing toward the screen when rotating
        // Consider using Unity events instead of constantly changing the rotation every frame
        
        float orientation = player.orientation;
        float angle = Mathf.Clamp(orientation * 180, -180, 0);
        Quaternion targetRot = Quaternion.Euler(0,angle,0);
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smooth);

        // if (orientation > 0)
        // {
        //     Quaternion targetRot = Quaternion.Euler(0,0,0);
        //     transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smooth);   
        // }
        // else if (orientation < 0)
        // {
        //     Quaternion targetRot = Quaternion.Euler(0,-180,0);
        //     transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smooth);      
        // }
    }
}
