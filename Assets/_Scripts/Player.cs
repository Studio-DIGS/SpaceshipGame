using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ObjectOnPath
{
    
    public float speed;
    public float acceleration;
    private Vector2 input;    
    public float orientation = 1; // -1 is left, +1 is right
    
    public Transform bullet;
    public float bulletVelocity;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    // Start is called before the first frame update
    void Start()
    {

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
        if (isDashing)
        {
            return;
        }

        //Gets player movement input and moves ship
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // get movement input
        orientation = Mathf.Clamp(orientation + (input.x * 2), -1, 1); // calculate the orientation (left or right) based on input
        move = Vector2.Lerp(move, input * speed, acceleration * Time.deltaTime);

        // Shooting script for player
        if (Input.GetKeyDown(KeyCode.Z))
        {
            fireBullet();
        }

        // Dashing script for player
        if (Input.GetKeyDown(KeyCode.X) && canDash)
        {
            StartCoroutine(dash());
        }
    }

    void _updateParticles()
    {
        return;
    }

    void fireBullet() 
    {
        Transform bulletTransform = Instantiate(bullet, transform.position, Quaternion.identity, transform.parent);
        float shootDir = bulletVelocity * orientation;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
    }

    private IEnumerator dash()
    {
        canDash = false;
        isDashing = true;

        move = input * dashingPower;

        yield return new WaitForSeconds(dashingTime);
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}