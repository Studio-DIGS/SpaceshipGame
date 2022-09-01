using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyClass : ObjectOnPath
{
    //public WaveSpawner waveSpawner; //Ant trying to communicate with WaveSpawner Inspector
    
    public int maxHealth;
    public float speed;
    public float acceleration;

    public HealthSystem healthSystem;
    public HealthBar healthBar;

    protected GameObject player;
    [HideInInspector] public float initialDirection;
    [HideInInspector] public Vector3 spawnPoint;

    private PathCreator pathCreator;

    public ParticleSystem explosion;

    // AudioSource[] allEnemyClassSounds; //Ant Creating host for AudioSounds
    // AudioSource enemyDeath1;
    // AudioSource enemyDeath2;
    // AudioSource enemyDeath3;
    
    protected virtual void Awake()
    {
        onPath = false;
        player = GameObject.Find("Player");
        pathCreator = GameObject.FindWithTag("WorldPath").GetComponent<PathCreator>();
        //allEnemyClassSounds = waveSpawner.GetComponents<AudioSource>(); //Ant Death variables gaining "Audio Source" properties from WaveSpawner Inspector
    }

    protected virtual void Start()
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
        float playerDistance = pathCreator.path.GetClosestDistanceAlongPath(player.transform.position);
        float enemySpawnDistance = playerDistance - 180f;
        if (enemySpawnDistance <= 0f)
        {
            enemySpawnDistance = playerDistance + 180f;
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
            initialDirection = -1 * player.GetComponent<Player>().getOrientation();
            onPath = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            Destroy(other.gameObject);
            this.healthSystem.Damage(1);
            if (this.healthSystem.GetHealth() <= 0)
            {
                //Death SFX Here

                // Death explosion goes here
                Debug.Log("im dead 💀");
                explosion.Play();
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                Destroy(this.gameObject, 1);
            }
        }
    }
}
