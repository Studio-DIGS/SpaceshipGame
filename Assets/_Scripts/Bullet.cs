using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;

public class Bullet : ObjectOnPath
{
    private float shootDir;

    public void Setup(float shootDir)
    {
        this.shootDir = shootDir;
    }

    void Update()
    {
        move.x = shootDir;
    }
}
