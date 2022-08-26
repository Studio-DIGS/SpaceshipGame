using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommandPattern;
using PathCreation;

public class Player : ObjectOnPath
{
    [SerializeField] PlayerMesh playerMesh;
    private PlayerStats playerStats;
    private Points points;
    public Camera playerCamera;
    public HealthSystem healthSystem;
    public HealthBar healthBar;

    private Vector2 input;    
    public float orientation = 1; // -1 is left, +1 is right
    private PathCreator pathCreator;

    //public Transform bullet;

    private bool canDash = true;
    private bool isDashing;
    private bool invincible;

    private FireCommand mainAttack;
    private float previousFire = 0.0f;

    private int layerMask; // Layer on Barrier
    public float rayDistance;

    public float distanceAlongPath;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        pathCreator = GameObject.FindWithTag("WorldPath").GetComponent<PathCreator>();
        healthSystem = new HealthSystem(playerStats.maxHealth);
        healthBar.Setup(healthSystem);
        points = GetComponent<Points>();
        mainAttack = (BasicAttack) ScriptableObject.CreateInstance("BasicAttack");
        layerMask = LayerMask.NameToLayer("Barrier");
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
        distanceAlongPath = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        
        if (isDashing)
        {
            _checkCollision();
            return;
        }

        //Gets player movement input and moves ship
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // get movement input
        if (!Input.GetButton("LockOrientation"))
        {
            orientation = Mathf.Clamp(orientation + (input.x * 2), -1, 1); // calculate the orientation (left or right) based on input
        }
        move = Vector2.Lerp(move, input * playerStats.speed, playerStats.acceleration * Time.deltaTime);
        _checkCollision();

        // Shooting script for player
        if (Input.GetButton("Fire1"))
        {
            mainAttack.Fire(this.GetComponent<Player>());
            return;
        }

        // Dashing script for player
        if (Input.GetButton("Dash") && canDash)
        {
            StartCoroutine(dash());
        }
    }

    void _updateParticles()
    {
        return;
    }

    // void FixedUpdate()
    // {
    //     _checkCollision();
    // }

    void _checkCollision()
    {
        
        Debug.DrawRay(transform.position, Vector3.up * rayDistance, Color.yellow);

        if (Physics.Raycast(transform.position, Vector3.up, rayDistance, layerMask))
        {
            if (isDashing) {StopCoroutine(dash());}
            move.y = Mathf.Min(move.y, -1f);
        }
        else if (Physics.Raycast(transform.position, -Vector3.up, rayDistance, layerMask))
        {
            if (isDashing) {StopCoroutine(dash());}
            move.y = Mathf.Max(move.y, 1f);
        }
    }


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

    public void TakeDamage()
    {
        playerMesh.TakeDamage();
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