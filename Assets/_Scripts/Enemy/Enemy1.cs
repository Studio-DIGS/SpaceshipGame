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
    private static Player playerComponent;

    private new void Awake() 
    {
        base.Awake();
        if (playerComponent == null)
        {
            playerComponent = player.GetComponent<Player>();
        }
    }

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

    // private void OnTriggerEnter(Collider other) //Overrides EnemyClass OnTriggerEnter
    // {
    //     if (other.gameObject.tag == "PlayerProjectile")
    //     {
    //         ParticleSystem sparks = (ParticleSystem) Instantiate(damageParticlePrefab, other.gameObject.transform.position, Quaternion.identity);
    //         sparks.Play();
    //         Destroy(other.gameObject);
    //         this.healthSystem.Damage(1);
    //         if (this.healthSystem.GetHealth() <= 0)
    //         {
    //             player.GetComponent<Player>().points.AddPoints(pointsWorth);
    //             enemyDeath.Play(); //Ant enemy death sound
    //             explosion.Play();
    //             gameObject.transform.localScale = new Vector3(0, 0, 0);
    //             this.gameObject.GetComponent<SphereCollider>().enabled = false;

    //             Destroy(this.gameObject, 1);
    //         }
    //     }
    //     if (other.gameObject.tag == "Player" && !playerComponent.invincible)
    //     {
    //         explosion.Play();
    //         Destroy(this.gameObject);
    //     }
    // }
}
