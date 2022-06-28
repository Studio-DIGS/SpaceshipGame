using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FormationPrefabScript : MonoBehaviour
{
    //GettingToPath Vars
    //private bool isOnPath = false;
    public GameObject player;
    private float direction;
    private bool isOnLeftSide = true;
    private GameObject path;
    private PathCreator pathCreator;
    private List<Enemy> enemy;
    [SerializeField] private float speed = 30.0f;

    private void Awake() {
        direction = player.GetComponent<Player>().orientation;
        path = GameObject.FindWithTag("WorldPath");
        pathCreator = path.GetComponent<PathCreator>();
    }

    void Start()
    {
        //isOnPath = false;
        if (transform.localPosition.x < 0) {
            isOnLeftSide = true;
        } else {
            isOnLeftSide = false;
        }

    }
    void Update()
    {
        float pathDist = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        float distance = transform.position.x - pathCreator.path.GetPointAtDistance(pathDist).x;
        if (Mathf.Abs(distance) >= 1) {
            if (isOnLeftSide) {
                foreach(Transform enemy in transform) {
                    Rigidbody enemyBody = enemy.GetComponent<Rigidbody>();
                    enemyBody.velocity = Vector3.right * speed;
                }
            } else {
                foreach(Transform enemy in transform) {
                    Rigidbody enemyBody = enemy.GetComponent<Rigidbody>();
                    enemyBody.velocity = Vector3.left * speed;
                }
            }
        }
    }
}
