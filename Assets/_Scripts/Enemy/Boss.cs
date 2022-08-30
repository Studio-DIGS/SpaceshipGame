using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyClass
{
    private Vector2 moveDirection;
    public float offsetRadius;
    public LayerMask targetMask;
    [HideInInspector] public int orientation; // -1 is left, +1 is right

    void Update()
    {
        if (!onPath)
        {
            MoveToPath(); // this function is found in EnemyClass
        }
        else
        {
            FollowPlayer();
            
            move = Vector2.Lerp(move, moveDirection * speed, acceleration * Time.deltaTime); // applies movement
        }
    }

    private void FollowPlayer()
    {
        target = player.transform.position;
        moveDirection = targetDir; // sets direction of movement to tracking player
        
        Collider[] hits = Physics.OverlapSphere(transform.position, offsetRadius, targetMask);
        if (hits.Length != 0)
        {
            moveDirection.x = -1 * targetDir.x; // keeps distance from player

            // Smooth movement to offset distance so that he's not bouncing back and fourth
            Vector3 delta = this.transform.position - hits[0].transform.position;
            moveDirection *= Mathf.Clamp((offsetRadius * offsetRadius - delta.sqrMagnitude), 0, 1);
        }
        
        if (targetDir.x > 0f) {orientation = 1;}
        else if (targetDir.x < 0f) {orientation = -1;}
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, offsetRadius);
    }
}
