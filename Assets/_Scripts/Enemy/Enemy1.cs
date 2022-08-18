using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyClass
{
    public bool isChasing;
    private Vector2 moveDirection;

    void Update()
    {
        if (!onPath)
        {
            MoveToPath();
        }
        else
        {
            target = player.transform.position;
            if (isChasing) {Chase();}
            else {Wander();}

            move = Vector2.Lerp(move, moveDirection * speed, acceleration * Time.deltaTime); // applies movement
        }
    }

    private void Wander()
    {
        // if (Mathf.Abs(distanceToTarget) <= detectionDistance)
        // {
        //     isChasing = true;
        // }
        // else
        // {
        //     target = new Vector2(initialDirection, 0f);
        // }
        moveDirection = new Vector2(initialDirection, 0f);
    }

    private void Chase()
    {
        moveDirection = targetDir;
    }

}
