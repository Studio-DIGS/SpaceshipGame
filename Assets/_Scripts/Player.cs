using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    
    public float speed;
    private Vector2 input;    
    public float orientation = 1; // -1 is left, +1 is right
    public GameObject projectile;
    public float bulletVelocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public float getOrientation()
    {
        return orientation;
    }

    // Update is called once per frame
    void Update()
    {
        _updatePlayer();
        _updateParticles();
    }

    void _updatePlayer()
    {
        /*
            Gets player movement input and moves ship
        */
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // get movement input
        orientation = Mathf.Clamp(orientation + (input.x * 2), -1, 1); // calculate the orientation (left or right) based on input
        Vector3 move = transform.forward * input.x + new Vector3(0,input.y,0); // get direction of movement based on forward direction
        controller.Move(move * Time.deltaTime * speed);
        Debug.DrawLine(transform.position, transform.position + move * speed, Color.blue);

        /*
            Shooting script for player
        */
        if (Input.GetKeyDown(KeyCode.Z))
        {
            fireBullet();
        }
    }

    void _updateParticles()
    {
        return;
    }

    void fireBullet() {
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity, transform.parent) as GameObject;
        if (orientation == -1)
        {
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVelocity * -1);
        } else
        {
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVelocity);
        }

        /*
        if (orientation == -1)
        {
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletVelocity * -1;
        } else
        {
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletVelocity;
        }
        */
    }
}