using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float shootDir;

    public void Setup(float shootDir)
    {
        this.shootDir = shootDir;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * shootDir * Time.deltaTime;
    }
}
