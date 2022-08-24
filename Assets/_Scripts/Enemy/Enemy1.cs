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

    private void OnTriggerEnter(Collider other) //Overrides EnemyClass OnTriggerEnter
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            Destroy(other.gameObject);
            this.healthSystem.Damage(1);
            if (this.healthSystem.GetHealth() <= 0)
            {
                // Death explosion goes here
                Destroy(this.gameObject);
            }
        }
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(DeathAnimation()); //Insert Kamikaze explosion here
        }
    }

    private IEnumerator DeathAnimation()
    {
        Destroy(GetComponent<SphereCollider>()); //This line is optional, in case the death animation is longer and we don't want the enemy to hit multiple times
        //Insert Kamikaze explosion here
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
