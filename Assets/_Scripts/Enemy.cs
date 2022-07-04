using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Enemy : MonoBehaviour
{
    //OnPath Vars
    private float direction;
    public GameObject player;
    private Rigidbody rigidBody;
    public float speed;
    private const int speed_to_force = 15;

    //GettingToPath Vars
    private bool isOnPath = false;
    private bool isOnLeftSide = true;
    private GameObject path;
    private PathCreator pathCreator;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        direction = player.GetComponent<Player>().orientation;
        path = GameObject.FindWithTag("WorldPath");
        pathCreator = path.GetComponent<PathCreator>();
    }
    void Start()
    {
        isOnPath = false;
        if (transform.localPosition.x < 0) {
            isOnLeftSide = true;
        } else {
            isOnLeftSide = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (!isOnPath) {

            float pathDist = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
            float distance = transform.position.x - pathCreator.path.GetPointAtDistance(pathDist).x;
            Debug.Log(distance);
            if (Mathf.Abs(distance) >= 1) {
                if (isOnLeftSide) {
                    rigidBody.velocity = Vector3.right * speed;
                } else {
                    rigidBody.velocity = Vector3.left * speed;
                }
            } else {
                gameObject.AddComponent<PathBound>();
                isOnPath = true;
            }

        } else {
            if (direction == -1) // Player facing left, therefore move right
            {
                rigidBody.velocity = transform.forward * speed;
            } else 
            {
                rigidBody.velocity = transform.forward * speed * -1;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
}