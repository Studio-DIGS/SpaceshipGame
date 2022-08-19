using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0 : EnemyClass
{
    private Vector2 moveDirection;

    void Update()
    {
        if (!onPath)
        {
            MoveToPath(); // this function is found in EnemyClass
        }
        else
        {
            Wander();

            move = Vector2.Lerp(move, moveDirection * speed, acceleration * Time.deltaTime); // applies movement
        }
    }

    private void Wander()
    {
        moveDirection = new Vector2(initialDirection, 0f);
    }
}
