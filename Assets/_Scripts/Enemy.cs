using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Enemy : MonoBehaviour
{
    //OnPath Vars
    private bool hasAddedComponents = false;
    private float direction;
    public GameObject player;
    private Rigidbody rigidBody;
    public float speed;
    private const int speed_to_force = 15;

    //GettingToPath Vars
    private FormationPrefabScript formationScript;
    private GameObject path;
    private PathCreator pathCreator;
    public bool isOnPath = false;

    private void Awake() {
        formationScript = transform.parent.GetComponent<FormationPrefabScript>();
        rigidBody = GetComponent<Rigidbody>();
        direction = player.GetComponent<Player>().orientation;
        path = GameObject.FindWithTag("WorldPath");
        pathCreator = path.GetComponent<PathCreator>();
    }

    void Update()
    {
        if (!isOnPath) 
        {
            return;
        } 
        else 
        {
            if (!hasAddedComponents)
            {
                putOnPath();
                hasAddedComponents = true;
                if (direction == -1) // Player facing left, therefore move right
                {
                    gameObject.GetComponent<ObjectOnPath>().move = new Vector2( 1, 0 ) * speed;
                } 
                else 
                {
                    gameObject.GetComponent<ObjectOnPath>().move = new Vector2( 1, 0 ) * speed * -1;
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

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
}
