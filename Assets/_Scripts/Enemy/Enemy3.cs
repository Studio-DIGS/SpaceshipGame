using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyClass
{
    private Vector2 moveDirection;
    private bool isNearCeiling = false;

    [SerializeField] LayerMask playerMask;

    private bool canFireLaser = true;
    [SerializeField] Laser laser;
    [SerializeField] float warningDuration = 1.0f;
    [SerializeField] float fireDuration = 2.0f;
    [SerializeField] float fireCooldown = 5.0f;
    [SerializeField] float bottomOfLaser = -50.0f;
    
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

            if (canFireLaser)
            {
                StartCoroutine(FireLaser());
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

    private IEnumerator FireLaser()
    {
        bool hasHitPlayer = false;
        float curTimer = 0.0f;
        canFireLaser = false;

        curTimer = warningDuration;
        laser.EnableWarningLine();
        while (curTimer >= 0.0f)
        {
        laser.DisplayWarningLine(this.transform.position, new Vector3(this.transform.position.x, bottomOfLaser, this.transform.position.z));
            curTimer -= Time.deltaTime;
            yield return null;
        }

        curTimer = fireDuration;
        laser.EnableLaserLine();
        while (curTimer >= 0.0f)
        {
            //Laser SFX Here
            laser.ShootLaser(this.transform.position, new Vector3(this.transform.position.x, bottomOfLaser, this.transform.position.z));
            if (Physics.Raycast(this.transform.position, Vector3.down, Mathf.Abs(this.transform.position.y - bottomOfLaser), playerMask) && !hasHitPlayer)
            {
                player.GetComponent<Player>().TakeDamage();
                hasHitPlayer = true;
            }

            curTimer -= Time.deltaTime;
            yield return null;
        }

        laser.RemoveLasers();
        yield return new WaitForSeconds(fireCooldown);

        canFireLaser = true;
    }

    private void OnTriggerEnter(Collider other) //Overrides EnemyClass OnTriggerEnter
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            laser.RemoveLasers();
            Destroy(other.gameObject);
            this.healthSystem.Damage(1);
            if (this.healthSystem.GetHealth() <= 0)
            {
                player.GetComponent<Player>().points.AddPoints(pointsWorth);

                explosion.Play();
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                this.gameObject.GetComponent<SphereCollider>().enabled = false;

                Destroy(this.gameObject, 1);
            }
        }
    }
}
