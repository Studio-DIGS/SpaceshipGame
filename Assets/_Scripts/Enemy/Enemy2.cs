using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyClass
{
    [SerializeField] float projectileSpeed = 25.0f;
    [SerializeField] float shootingCooldown = 2.5f;
    private static Object bulletPrefab;

    private Vector2 moveDirection;
    private bool canShoot = true;
    
    //Ant Audio
    AudioSource[] allEnemy2Sounds;
    AudioSource enemy2Death;
    AudioSource enemy2bullet;


    private new void Awake()
    {
        allEnemy2Sounds = GetComponents<AudioSource>(); //Ant
        enemy2Death = allEnemy2Sounds[0]; //Ant
        enemy2bullet = allEnemy2Sounds[1]; //Ant

        base.Awake();
        if (bulletPrefab == null)
        {
            bulletPrefab = Resources.Load<Bullet>("Prefabs/EnemyBullet");
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
            Wander();
            if(canShoot && isAlive)
            {
                StartCoroutine(Shoot());
            }

            move = Vector2.Lerp(move, moveDirection * speed, acceleration * Time.deltaTime); // applies movement
            
        }
    }

    private void Wander()
    {
        moveDirection = new Vector2(initialDirection, 0f);
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        FireBullets();

        yield return new WaitForSeconds(shootingCooldown);
        canShoot = true;

    }

    private void FireBullets()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Bullet[] projectiles = new Bullet [4];
        for (int i = 0; i < 4; i++)
        {
            Bullet curBullet = (Bullet) Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            projectiles[i] = curBullet;
        }
        projectiles[0].Setup(projectileSpeed, projectileSpeed);
        projectiles[1].Setup(projectileSpeed, -projectileSpeed);
        projectiles[2].Setup(-projectileSpeed, -projectileSpeed);
        projectiles[3].Setup(-projectileSpeed, projectileSpeed);

        enemy2bullet.Play();
    }
}
