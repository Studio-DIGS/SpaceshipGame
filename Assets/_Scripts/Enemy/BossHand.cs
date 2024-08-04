using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : EnemyClass
{
    public bool leftHand;
    public float frequency;
    public float amplitude;
    private float phaseShift;
    private float timer;
    public Boss boss;
    public float offsetFromHead;
    private Vector2 moveDirection;
    [HideInInspector] public float orientation;

    public float projectileSpeed = 25.0f;
    public float shootinCooldown = 2.5f;
    [SerializeField] private GameObject bulletPrefab;
    private bool canShoot = true;

    private new void Awake()
    {   
        base.Awake();
        if (leftHand)
        {
            phaseShift = 1;
        }
    }

    void Update()
    {
        if (!onPath)
        {
            MoveToPath(); // function found in EnemyClass
        }
        else if (boss != null)
        {
            Circling();
            if(canShoot)
            {
                StartCoroutine(Shoot());
            }
            move = Vector2.Lerp(move, moveDirection * speed, acceleration * Time.deltaTime);
        }
        else
        {
            explosion.Play();
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            Destroy(this.gameObject, 1);
        }
    }

    private void Circling()
    {   
        timer += Time.deltaTime;

        target = boss.transform.position + Vector3.up * oscillate(timer, frequency, amplitude);
        offset = -orientation * (offsetFromHead + 5f * phaseShift);

        moveDirection = targetDir;

        this.orientation = boss.orientation;
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        FireBullet();

        yield return new WaitForSeconds(shootinCooldown);
        canShoot = true;
    }

    private void FireBullet()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        var _projectile = Instantiate (bulletPrefab, this.transform.position + orientation * transform.forward * 2f, Quaternion.identity);
        var _bullet = _projectile.GetComponent<Bullet>();
        _bullet.Setup(projectileSpeed * this.orientation);
    }

    float oscillate(float time, float speed, float scale)
    {
        return Mathf.Cos(time * speed / Mathf.PI + (phaseShift * Mathf.PI)) * scale;
    }

    private void OnTriggerEnter(Collider other) //overrides EnemyClass OnTriggerEnter
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            Destroy(other.gameObject);
        }
    }
}
