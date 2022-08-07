using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Enemy : MonoBehaviour
{
    //OnPath Vars
    private bool hasAddedComponents = false;
    private float direction;
    private GameObject player;
    private Rigidbody rigidBody;
    public float speed;
    private const int speed_to_force = 15;

    //GettingToPath Vars
    private FormationPrefabScript formationScript;
    private GameObject path;
    private PathCreator pathCreator;
    public bool isOnPath = false;

    //Detection Radius Vars
    SphereCollider detectionCircle;
    [SerializeField] float radius = 25.0f;
    private bool isChasingPlayer = false;
    private bool isLeft = false;

    //Chasing Player Vars
    private ObjectOnPath enemyMovement;

    //Points Vars
    public int health = 3;
    [SerializeField] int enemyDamage = 1;
    [SerializeField] int pointsWorth = 100;

    private void Awake() {
        player = GameObject.Find("Player");
        formationScript = transform.parent.GetComponent<FormationPrefabScript>();
        rigidBody = GetComponent<Rigidbody>();
        direction = player.GetComponent<Player>().orientation;
        path = GameObject.FindWithTag("WorldPath");
        pathCreator = path.GetComponent<PathCreator>();
    }

    void Update()
    {
        
        if (isChasingPlayer)
        {   
            float enemyDistance = pathCreator.path.GetClosestDistanceAlongPath(this.gameObject.transform.position);
            float playerDistance = player.GetComponent<Player>().distanceAlongPath;
            float heightDifference = player.transform.position.y - this.gameObject.transform.position.y;
            float distance = playerDistance - enemyDistance;
            if (distance >= 0f && distance <= 150f)
            {
                //Enemy is left
                isLeft = true;
            }
            else if (distance <= 0f && distance >= -150f)
            {
                //Enemy is right
                isLeft = false;
            }
            if (isLeft)
            {
                enemyMovement.move = new Vector2(1, heightDifference).normalized * speed;
            }
            else
            {
                enemyMovement.move = new Vector2(-1, heightDifference).normalized * speed;
            }

            return;
        }
        

        if (!isOnPath) 
        {
            return;
        } 
        else 
        {
            //Puts Enemy On Path And Adds Detection Circle
            if (!hasAddedComponents)
            {
                putOnPath();
                hasAddedComponents = true;
                enemyMovement = gameObject.GetComponent<ObjectOnPath>();
                if (direction == -1) // Player facing left, therefore move right
                {
                    enemyMovement.move = new Vector2( 1, 0 ) * speed;
                } 
                else 
                {
                    enemyMovement.move = new Vector2( 1, 0 ) * speed * -1;
                }

                gameObject.AddComponent<SphereCollider>();
                detectionCircle = gameObject.GetComponent<SphereCollider>();
                if (detectionCircle != null)
                {
                    detectionCircle.isTrigger = true;
                    detectionCircle.transform.position = this.gameObject.transform.position;
                    detectionCircle.radius = radius;
                }
            }

        }
    }

    void putOnPath()
    {
        gameObject.AddComponent<PathBound>();
        gameObject.AddComponent<ObjectOnPath>();
        gameObject.GetComponent<PathBound>().objectOnPath = gameObject.GetComponent<ObjectOnPath>();
    }

    
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            Destroy(other.gameObject);
            health--;
            if (health <= 0)
            {
                player.GetComponent<Points>().AddPoints((int)Math.Floor(pointsWorth * formationScript.multiplier));
                Destroy(gameObject);  
            }
        }
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().healthSystem.Damage(enemyDamage);
            if (player.GetComponent<Player>().healthSystem.GetHealth() == 0)
            {
                player.transform.GetChild(0).gameObject.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == player.gameObject.tag)
        {
            Destroy(detectionCircle);
            isChasingPlayer = true;
            //Debug.Log("Player in radius");
        }
    }
    
    /*
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == player.gameObject.tag)
        {
            ChasePlayer(other.gameObject);
        }
    }

    private void ChasePlayer(GameObject _player)
    {
        Vector3 chaseDirection = transform.position - _player.transform.position;
        enemyMovement.move = chaseDirection;
    }
    */
}
