using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyClass : ObjectOnPath
{
    public int maxHealth;
    public float speed;
    public float acceleration;

    public HealthSystem healthSystem;
    public HealthBar healthBar;

    private GameObject player;
    [HideInInspector] public float initialDirection;
    [HideInInspector] public Vector3 spawnPoint;

    private PathCreator pathCreator;

    protected virtual void Awake()
    {
        onPath = false;
        player = GameObject.Find("Player");
        pathCreator = GameObject.FindWithTag("WorldPath").GetComponent<PathCreator>();
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
        float enemySpawnDistance = Mathf.Abs(playerDistance - 180f); // TODO: Spawn Enemy across from player
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
                // Death explosion goes here
                Destroy(this.gameObject);
            }
        }
    }
}
