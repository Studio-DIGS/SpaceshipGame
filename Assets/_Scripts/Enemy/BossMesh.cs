using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMesh : MonoBehaviour
{
    public float smooth;
    public float maxAngle;
    public Boss boss;

    void Update()
    {
        float angle = Mathf.Clamp(-1 * boss.orientation * (180 - maxAngle), maxAngle, (180- maxAngle));
        Quaternion targetRot = Quaternion.Euler(0,angle,0);
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smooth);
    }
}
