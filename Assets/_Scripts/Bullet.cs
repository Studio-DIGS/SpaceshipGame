using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;

public class Bullet : ObjectOnPath
{
    private Vector2 movementDirection;

    public void Setup(float xSpeed)
    {
        movementDirection = new Vector2(xSpeed, 0);
    }
    
    public void Setup(float xSpeed, float ySpeed)
    {
        movementDirection = new Vector2(xSpeed, ySpeed);
    }

    void Update()
    {
        move = movementDirection;
    }
}
