using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public abstract class EnemyClass : ObjectOnPath
{
    public Player player2; //Ant pulling from player script

    public int maxHealth;
    public float speed;
    public float acceleration;
    public int pointsWorth;

    public HealthSystem healthSystem;
    public HealthBar healthBar;

    protected static GameObject player;
    [HideInInspector] public float initialDirection;
    [HideInInspector] public Vector3 spawnPoint;

    private static PathCreator pathCreator;

    public GameObject damageParticlePrefab;
    public ParticleSystem explosion;
    private Formation parentFormation;
    protected bool isAlive = true;

    public AudioSource enemyDeath;

    protected virtual void Awake()
    {
        //allEnemyClassSounds = player2.allPlayerSounds; //Ant
        //enemy0Death = allEnemyClassSounds[3]; //Ant

        onPath = false;
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        if (pathCreator == null)
        {
            pathCreator = GameObject.FindWithTag("WorldPath").GetComponent<PathCreator>();
        }
        parentFormation = this.transform.parent.gameObject.GetComponent<Formation>();
    }

    public virtual void Start()
    {
        healthSystem = new HealthSystem(maxHealth);
        if (healthBar != null)
        {
            healthBar.Setup(healthSystem);
        }

        FindPointOnPath();
    }

    private void FindPointOnPath()
    {
        float enemySpawnDistance = parentFormation.playerDistance - 180f;
        if (enemySpawnDistance <= 0f)
        {
            enemySpawnDistance = parentFormation.playerDistance + 180f;
        }
        spawnPoint = pathCreator.path.GetPointAtDistance(enemySpawnDistance);
        spawnPoint.y = transform.position.y;
    }

    public void MoveToPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnPoint, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, spawnPoint) < 0.01f)
        {
            GetComponent<PathBound>().enabled = true;
            if (parentFormation.direction != 1.0f || parentFormation.direction != -1.0f)
            {
                initialDirection = parentFormation.direction;
            }
            else
            {
                initialDirection = -1 * player.GetComponent<Player>().orientation;
                Debug.LogError("parentFormation.direction not working!");
            }
            onPath = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            var sparks = Instantiate(damageParticlePrefab, other.gameObject.transform.position, Quaternion.identity);
            sparks.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
            this.healthSystem.Damage(1);
        }
        else if (other.gameObject.tag == "Player" && !player.GetComponent<Player>().invincible)
        {
            this.healthSystem.Damage(1);
        }
        
        if (this.healthSystem.GetHealth() <= 0)
        {
            player.GetComponent<Player>().points.AddPoints(pointsWorth);
            enemyDeath.Play(); //Ant enemy death sound
            explosion.Play();
            isAlive = false;
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            this.gameObject.GetComponent<SphereCollider>().enabled = false;

            Destroy(this.gameObject, 1);
        }
    }
}
