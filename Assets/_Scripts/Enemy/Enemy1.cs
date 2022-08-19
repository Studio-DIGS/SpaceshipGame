using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyClass
{
    public bool isChasing;
    private Vector2 moveDirection;
    public float detectionRadius;
    public LayerMask targetMask;
    private Transform targetTransform;

    void Update()
    {
        if (!onPath)
        {
            MoveToPath(); // this function is found in EnemyClass
        }
        else
        {
            if (isChasing) {Chase();}
            else {Wander();}

            move = Vector2.Lerp(move, moveDirection * speed, acceleration * Time.deltaTime); // applies movement
        }
    }

    private void Wander()
    {
        moveDirection = new Vector2(initialDirection, 0f);

        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, targetMask);
        if (hits.Length != 0)
        {
            targetTransform = hits[0].transform;
            isChasing = true;
        }
    }

    private void Chase()
    {
        target = targetTransform.position;
        moveDirection = targetDir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
        }
    }
}
