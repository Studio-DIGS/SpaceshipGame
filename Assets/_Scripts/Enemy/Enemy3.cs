using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyClass
{
    private Vector2 moveDirection;
    private bool isNearCeiling = false;
    
    [SerializeField] float ceilingDetectionRadius;
    [SerializeField] LayerMask ceilingMask;

    [SerializeField] float hoveringHeight;

    void Update()
    {
        if (!onPath)
        {
            MoveToPath(); // this function is found in EnemyClass
        }
        else
        {
            if (!isNearCeiling)
            {
                GoTowardsCeiling();
                CheckIfNearCeiling();
            }
            else
            {
                Wander();
            }

            //FireLazer Here

            move = Vector2.Lerp(move, moveDirection * speed, acceleration * Time.deltaTime); // applies movement
        }
    }

    private void GoTowardsCeiling()
    {
        moveDirection = new Vector2(initialDirection, 1f);
    }

    private void CheckIfNearCeiling()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, ceilingDetectionRadius, ceilingMask);

        if (hits.Length != 0)
        {
            isNearCeiling = true;
        }
    }

    private void Wander()
    {
        if (this.transform.position.y > hoveringHeight)
        {
            moveDirection = new Vector2(initialDirection, -0.25f);
        }
        else
        {
            move.y = 0f;
            moveDirection = new Vector2(initialDirection, 0f);
        }
    }
}
