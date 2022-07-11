using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommandPattern;

public class Player : ObjectOnPath
{
    private PlayerStats playerStats;
    private Points points;

    private Vector2 input;    
    public float orientation = 1; // -1 is left, +1 is right

    //public Transform bullet;

    private bool canDash = true;
    private bool isDashing;

    private FireCommand mainAttack;
    private float previousFire = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        points = GetComponent<Points>();
        mainAttack = (BasicAttack) ScriptableObject.CreateInstance("BasicAttack");
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
        previousFire += Time.deltaTime;
        
        if (isDashing)
        {
            return;
        }

        //Gets player movement input and moves ship
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // get movement input
        orientation = Mathf.Clamp(orientation + (input.x * 2), -1, 1); // calculate the orientation (left or right) based on input
        move = Vector2.Lerp(move, input * playerStats.speed, playerStats.acceleration * Time.deltaTime);

        // Shooting script for player
        if (Input.GetKey(KeyCode.Z))
        {
            mainAttack.Fire(this.GetComponent<Player>());
            return;
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

    /*
    void fireBullet() 
    {
        Transform bulletTransform = Instantiate(bullet, transform.position, Quaternion.identity, transform.parent);
        float shootDir = playerStats.bulletVelocity * orientation;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
    }
    */

    private IEnumerator dash()
    {
        canDash = false;
        isDashing = true;

        move = input * playerStats.dashingPower;

        yield return new WaitForSeconds(playerStats.dashingTime);
        isDashing = false;

        yield return new WaitForSeconds(playerStats.dashingCooldown);
        canDash = true;
    }

    public float GetPreviousFire()
    {
        return previousFire;
    }

    public void SetPreviousFire(float _previousFire)
    {
        previousFire = _previousFire;
    }

    public float GetPlayerSpeed() 
    {
        return playerStats.speed;
    }
}