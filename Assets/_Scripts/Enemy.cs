using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float direction;
    public GameObject player;
    private Rigidbody rigidBody;
    public float speed;

    private const int speed_to_force = 15;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        direction = player.GetComponent<Player>().orientation;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == -1) // Player facing left, therefore move right
        {
            rigidBody.velocity = transform.forward * speed;
        } else 
        {
            rigidBody.velocity = transform.forward * speed * -1;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
}
